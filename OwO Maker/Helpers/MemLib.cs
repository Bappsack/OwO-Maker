using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace OwOMaker.Helpers
{
    public class Mem
    {
        public string wName = string.Empty;
        public IntPtr Handle = IntPtr.Zero;
        public RECT GameSize;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, nint nSize, out nint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, nint dwSize, out nint lpNumberOfBytesRead);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        static extern nint GetWindowThreadProcessId(IntPtr hWnd, out nint lpdwProcessId);

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public Process Proc;
        private ProcessModule MainModule;
        public IntPtr BaseAddress;

        public void Init(Process process)
        {
            try
            {
                Proc = process;
                MainModule = Proc.MainModule;
                BaseAddress = MainModule.BaseAddress;
                GetGameSize();
            }
            catch
            {
                throw new Exception("Failed to initialize memory class.");
            }
        }

        public void GetGameSize()
        {
            GetWindowRect(Proc.MainWindowHandle, out GameSize);
        }

        public IntPtr GetProcHandle()
        {
            try { return Proc.Handle; } catch { return IntPtr.Zero; }
        }

        public IntPtr GetModuleBaseAddress(Process proc, string modName)
        {
            IntPtr addr = IntPtr.Zero;

            foreach (ProcessModule m in proc.Modules)
                if (m.ModuleName.Equals(modName, StringComparison.OrdinalIgnoreCase))
                    return m.BaseAddress;

            return addr;
        }

        public Process FindProcessByHandle(IntPtr handle)
        {
            GetWindowThreadProcessId(handle, out var pID);
            return Process.GetProcessById((int)pID);
        }

        public IntPtr ReadPointer(IntPtr Address, nint[] offsets = null)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(GetProcHandle(), Address, buffer, 4, out _);

            if (offsets != null)
            {
                for (int i = 0; i < offsets.Length - 1; i++)
                {
                    Address = (IntPtr)(BitConverter.ToInt32(buffer, 0) + offsets[i]);
                    ReadProcessMemory(GetProcHandle(), Address, buffer, 4, out _);
                }

                Address = (IntPtr)(BitConverter.ToInt32(buffer, 0) + offsets[^1]);
            }
            else
            {
                Address = (IntPtr)BitConverter.ToInt32(buffer, 0);
            }

            return Address;
        }

        public T ReadMemory<T>(IntPtr Address, nint[] Offsets = null) where T : struct
        {
            if (Offsets != null)
                Address = ReadPointer(Address, Offsets);

            int ByteSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[ByteSize];
            ReadProcessMemory(GetProcHandle(), Address, buffer, (nint)ByteSize, out _);

            return ByteArrayToStructure<T>(buffer);
        }
        public byte[] ReadMemoryData(IntPtr Address, nint[] Offsets = null, int size = 0)
        {
            if (Offsets != null)
                Address = ReadPointer(Address, Offsets);

            int ByteSize = size;
            byte[] buffer = new byte[ByteSize];
            ReadProcessMemory(GetProcHandle(), Address, buffer, (nint)buffer.Length, out _);

            return buffer;
        }


        public void WriteMemory<T>(IntPtr Address, object Value, nint[] Offsets = null)
        {
            if (Offsets != null)
                Address = ReadPointer(Address, Offsets);

            byte[] buffer = StructureToByteArray(Value);
            WriteProcessMemory(GetProcHandle(), Address, buffer, (nint)buffer.Length, out _);
        }


        private T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        private byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);
            byte[] arr = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }


        public List<IntPtr> FindPatterns(string pattern)
        {
            var sigScan = new SigScan(Proc, Proc.MainModule.BaseAddress, Proc.MainModule.ModuleMemorySize);
            var arrayOfBytes = pattern.Split(' ').Select(b => b.Contains("?") ? (byte)0 : (byte)Convert.ToInt32(b, 16)).ToArray();
            var strMask = string.Join("", pattern.Split(' ').Select(b => b.Contains("?") ? '?' : 'x'));
            return sigScan.FindPatterns(arrayOfBytes, strMask, 0);
        }

        public nint FindPattern(string pattern)
        {
            var aob = FindPatterns(pattern);

            if (aob.Count <= 0)
                return 0;
            else
                return aob[0];
        }

        public IntPtr FindPattern(string pattern, int instruction, int Append)
        {
            var aob = FindPatterns(pattern);

            if (aob.Count <= 0)
                return 0;

            var offset = ReadMemory<int>(aob[0] + instruction);
            var result = aob[0] + Append + offset;

            return result;
        }
    }
}
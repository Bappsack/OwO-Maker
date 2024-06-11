using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OwOMaker.Helpers
{
    public class SignatureEntity
    {
        public int StartAddress { get; set; }
        public int SearchRange { get; set; }
        public byte[] WantedBytes { get; set; }
        public string Mask { get; set; }
        public int AddressOffset { get; set; }

        public SignatureEntity(int startSAddress, int searchRange, byte[] wantedBytes, string mask, int addressOffset)
        {
            StartAddress = startSAddress;
            SearchRange = searchRange;
            WantedBytes = wantedBytes;
            Mask = mask;
            AddressOffset = addressOffset;
        }

        public nint ScanSignature(Process process)
        {
            SigScan sigScan = new SigScan(process, new nint(StartAddress), SearchRange);
            return sigScan.FindPattern(WantedBytes, Mask, AddressOffset);
        }
    }


    public class SigScan
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(
            nint hProcess,
            nint lpBaseAddress,
            [Out()] byte[] lpBuffer,
            int dwSize,
            out int lpNumberOfBytesRead
            );

        public byte[] m_vDumpedRegion;

        private Process m_vProcess;

        private nint m_vAddress;

        private int m_vSize;

        public SigScan()
        {
            m_vProcess = null;
            m_vAddress = nint.Zero;
            m_vSize = 0;
            m_vDumpedRegion = null;
        }
        public SigScan(Process proc, nint addr, int size)
        {
            m_vProcess = proc;
            m_vAddress = addr;
            m_vSize = size;
        }
        public bool DumpMemory()
        {
            try
            {
                if (m_vProcess == null)
                    return false;
                if (m_vProcess.HasExited == true)
                    return false;
                if (m_vAddress == nint.Zero)
                    return false;
                if (m_vSize == 0)
                    return false;

                m_vDumpedRegion = new byte[m_vSize];

                bool bReturn = false;
                int nBytesRead = 0;

                bReturn = ReadProcessMemory(
                    m_vProcess.Handle, m_vAddress, m_vDumpedRegion, m_vSize, out nBytesRead
                    );

                if (bReturn == false || nBytesRead != m_vSize)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool MaskCheck(int nOffset, byte[] btPattern, string strMask)
        {
            for (int x = 0; x < btPattern.Length; x++)
            {
                if (strMask[x] == '?')
                    continue;

                if (strMask[x] == 'x' && btPattern[x] != m_vDumpedRegion[nOffset + x])
                    return false;
            }

            return true;
        }

        public nint FindPattern(byte[] btPattern, string strMask, int nOffset)
        {
            try
            {
                if (m_vDumpedRegion == null || m_vDumpedRegion.Length == 0)
                {
                    if (!DumpMemory())
                        return nint.Zero;
                }

                if (strMask.Length != btPattern.Length)
                    return nint.Zero;

                for (int x = 0; x < m_vDumpedRegion.Length - strMask.Length; x++)
                {
                    if (MaskCheck(x, btPattern, strMask))
                        return nint.Add(m_vAddress, x + nOffset);
                }

                return nint.Zero;
            }
            catch (Exception)
            {
                return nint.Zero;
            }
        }
        public List<nint> FindPatterns(byte[] btPattern, string strMask, int nOffset)
        {
            var ptrs = new List<nint>();
            try
            {
                if (m_vDumpedRegion == null || m_vDumpedRegion.Length == 0)
                {
                    if (!DumpMemory())
                        return null;
                }

                if (strMask.Length != btPattern.Length)
                    return null;

                for (int x = 0; x < m_vDumpedRegion.Length; x++)
                {
                    if (MaskCheck(x, btPattern, strMask))
                        ptrs.Add(nint.Add(m_vAddress, x + nOffset));
                }

                return ptrs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void ResetRegion()
        {
            m_vDumpedRegion = null;
        }
        public Process Process
        {
            get { return m_vProcess; }
            set { m_vProcess = value; }
        }
        public nint Address
        {
            get { return m_vAddress; }
            set { m_vAddress = value; }
        }
        public int Size
        {
            get { return m_vSize; }
            set { m_vSize = value; }
        }
    }
}
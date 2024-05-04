using System.Runtime.InteropServices;

namespace Toggl.Track.Interactive
{
    // From https://www.pinvoke.net/default.aspx/Structures/OPENFILENAME.html
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct OpenFileName
    {
        public int lStructSize;
        public IntPtr hwndOwner;
        public IntPtr hInstance;
        public string lpstrFilter;
        public string lpstrCustomFilter;
        public int nMaxCustFilter;
        public int nFilterIndex;
        public string lpstrFile;
        public int nMaxFile;
        public string lpstrFileTitle;
        public int nMaxFileTitle;
        public string lpstrInitialDir;
        public string lpstrTitle;
        public int Flags;
        public short nFileOffset;
        public short nFileExtension;
        public string lpstrDefExt;
        public IntPtr lCustData;
        public IntPtr lpfnHook;
        public string lpTemplateName;
        public IntPtr pvReserved;
        public int dwReserved;
        public int flagsEx;
    }

    public class FileDialog
    {
        // From https://www.pinvoke.net/default.aspx/comdlg32/GetOpenFileName.html
        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetOpenFileName(ref OpenFileName ofn);

        // From https://www.pinvoke.net/default.aspx/comdlg32/GetSaveFileName.html
        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetSaveFileName(ref OpenFileName ofn);

        private static OpenFileName CreateRequest(string title, string filter)
        {
            var ofn = new OpenFileName();
            ofn.lStructSize = Marshal.SizeOf(ofn);
            ofn.lpstrFilter = filter;
            ofn.lpstrFile = new string(new char[256]);
            ofn.nMaxFile = ofn.lpstrFile.Length;
            ofn.lpstrFileTitle = new string(new char[64]);
            ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
            ofn.lpstrTitle = title;
            return ofn;
        }

        public static string ShowOpen(string title = "Open", string filter = "All Files (*.*)\0*.*\0")
        {
            var ofn = CreateRequest(title, filter);
            return GetOpenFileName(ref ofn) ? ofn.lpstrFile : string.Empty;
        }

        public static string ShowSave(string title = "Save", string filter = "All Files (*.*)\0*.*\0")
        {
            var ofn = CreateRequest(title, filter);
            return GetSaveFileName(ref ofn) ? ofn.lpstrFile : string.Empty;
        }
    }
}


using System.Security.AccessControl;

namespace FolderSync.Helper
{
    internal class FileSecurityHandler
    {
        //// Adds an ACL entry on the specified file for the specified account.
        //public static void AddFileSecurity(string fileName, string account,
        //    FileSystemRights rights, AccessControlType controlType)
        //{

        //    // Get a FileSecurity object that represents the
        //    // current security settings.
        //    FileSecurity fSecurity = File.GetAccessControl(fileName);

        //    // Add the FileSystemAccessRule to the security settings.
        //    fSecurity.AddAccessRule(new FileSystemAccessRule(account,
        //        rights, controlType));

        //    // Set the new access settings.
        //    File.SetAccessControl(fileName, fSecurity);
        //}

        //// Removes an ACL entry on the specified file for the specified account.
        //public static void RemoveFileSecurity(string fileName, string account,
        //    FileSystemRights rights, AccessControlType controlType)
        //{

        //    // Get a FileSecurity object that represents the
        //    // current security settings.
        //    FileSecurity fSecurity = File.GetAccessControl(fileName);

        //    // Remove the FileSystemAccessRule from the security settings.
        //    fSecurity.RemoveAccessRule(new FileSystemAccessRule(account,
        //        rights, controlType));

        //    // Set the new access settings.
        //    File.SetAccessControl(fileName, fSecurity);
        //}
    }
}

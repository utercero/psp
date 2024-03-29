﻿using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;

namespace Permisos
{
    class MyClass
    {
        public static void PermissionSetDemo()
        {
            EnvironmentPermission.
            Console.WriteLine("Executing PermissionSetDemo");
            try
            {
                // Open a new PermissionSet.
                PermissionSet ps1 = new PermissionSet(PermissionState.None);
                Console.WriteLine("Adding permission to open a file from a file dialog box.");
                // Add a permission to the permission set.
                ps1.AddPermission(
                    new FileDialogPermission(FileDialogPermissionAccess.Open));
                Console.WriteLine("Demanding permission to open a file.");
                ps1.Demand();
                Console.WriteLine("Demand succeeded.");
                Console.WriteLine("Adding permission to save a file from a file dialog box.");
                ps1.AddPermission(
                    new FileDialogPermission(FileDialogPermissionAccess.Save));
                Console.WriteLine("Demanding permission to open and save a file.");
                ps1.Demand();
                Console.WriteLine("Demand succeeded.");
                Console.WriteLine("Adding permission to read environment variable USERNAME.");
                Console.WriteLine("USUARIO: {0}", Environment.GetEnvironmentVariable("USERNAME"));

                ps1.AddPermission(
                    new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME"));
                ps1.Demand();
                Console.WriteLine("USUARIO: {0}", Environment.GetEnvironmentVariable("USERNAME"));
                Console.WriteLine("Demand succeeded.");
                Console.WriteLine("Adding permission to read environment variable COMPUTERNAME.");
                ps1.AddPermission(
                    new EnvironmentPermission(EnvironmentPermissionAccess.Read, "COMPUTERNAME"));
                // Demand all the permissions in the set.
                Console.WriteLine("Demand all permissions.");
                ps1.Demand();
                Console.WriteLine("Demand succeeded.");
                // Display the number of permissions in the set.
                Console.WriteLine("Number of permissions = " + ps1.Count);
                // Display the value of the IsSynchronized property.
                Console.WriteLine("IsSynchronized property = " + ps1.IsSynchronized);
                // Display the value of the IsReadOnly property.
                Console.WriteLine("IsReadOnly property = " + ps1.IsReadOnly);
                // Display the value of the SyncRoot property.
                Console.WriteLine("SyncRoot property = " + ps1.SyncRoot);
                // Display the result of a call to the ContainsNonCodeAccessPermissions method.
                // Gets a value indicating whether the PermissionSet contains permissions
                // that are not derived from CodeAccessPermission.
                // Returns true if the PermissionSet contains permissions that are not
                // derived from CodeAccessPermission; otherwise, false.
                Console.WriteLine("ContainsNonCodeAccessPermissions method returned " +
                    ps1.ContainsNonCodeAccessPermissions());
                Console.WriteLine("Value of the permission set ToString = \n" + ps1.ToString());
                PermissionSet ps2 = new PermissionSet(PermissionState.None);
                // Create a second permission set and compare it to the first permission set.
                ps2.AddPermission(
                    new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME"));
                ps2.AddPermission(
                    new EnvironmentPermission(EnvironmentPermissionAccess.Write, "COMPUTERNAME"));
                IEnumerator list = ps1.GetEnumerator();
                Console.WriteLine("Permissions in first permission set:");
                while (list.MoveNext())
                    Console.WriteLine(list.Current.ToString());
                Console.WriteLine("Second permission IsSubsetOf first permission = " + ps2.IsSubsetOf(ps1));
                // Display the intersection of two permission sets.
                PermissionSet ps3 = ps2.Intersect(ps1);
                Console.WriteLine("The intersection of the first permission set and "
                    + "the second permission set = " + ps3.ToString());
                // Create a new permission set.
                PermissionSet ps4 = new PermissionSet(PermissionState.None);
                ps4.AddPermission(
                    new FileIOPermission(FileIOPermissionAccess.Read,
                    "C:\\Temp\\Testfile.txt"));
                ps4.AddPermission(
                    new FileIOPermission(FileIOPermissionAccess.Read |
                    FileIOPermissionAccess.Write | FileIOPermissionAccess.Append,
                    "C:\\Temp\\Testfile.txt"));
                // Display the union of two permission sets.
                PermissionSet ps5 = ps3.Union(ps4);
                Console.WriteLine("The union of permission set 3 and permission set 4 = "
                    + ps5.ToString());
                // Remove FileIOPermission from the permission set.
                ps5.RemovePermission(typeof(FileIOPermission));
                Console.WriteLine("The last permission set after removing FileIOPermission = "
                    + ps5.ToString());
                // Change the permission set using SetPermission.
                ps5.SetPermission(new EnvironmentPermission(EnvironmentPermissionAccess.AllAccess, "USERNAME"));
                Console.WriteLine("Permission set after SetPermission = " + ps5.ToString());
                // Display result of ToXml and FromXml operations.
                PermissionSet ps6 = new PermissionSet(PermissionState.None);
                ps6.FromXml(ps5.ToXml());
                Console.WriteLine("Result of ToFromXml = " + ps6.ToString() + "\n");
                // Display results of PermissionSet.GetEnumerator.
                IEnumerator psEnumerator = ps1.GetEnumerator();
                while (psEnumerator.MoveNext())
                {
                    Console.WriteLine(psEnumerator.Current);
                }
                // Check for an unrestricted permission set.
                PermissionSet ps7 = new PermissionSet(PermissionState.Unrestricted);
                Console.WriteLine("Permission set is unrestricted = " + ps7.IsUnrestricted());
                // Create and display a copy of a permission set.
                ps7 = ps5.Copy();
                Console.WriteLine("Result of copy = " + ps7.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

        }
        static void Main(string[] args)
        {
            PermissionSetDemo();
        }
    }
}
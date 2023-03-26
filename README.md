# FolderSync
FolderSync is an application that synchronizes a replica folder with the content of a given source folder.  
The algorithm periodically reads the content of the source and the replica. If any of them have been altered, the algorithm grabs the changes and applies them to the replica folder.  
The algorithm logs all changes on the specified path, e.g., `C:\user\Documents\logFile.txt`.  
Currently, the algorithm replaces all the content of a detected folder with changes. It is possible to change this behavior in the future by using a different data structure to hold the changes, like a Dictionary.  
It's possible to use the program with console arguments like this: On PowerShell, add `syncFolder.exe sourcePath replicaPath logPath PeriodicityInSeconds`.

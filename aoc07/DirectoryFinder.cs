using System.Collections.Immutable;

public static class DirectoryFinder {

    public static IEnumerable<Directory> DirectoriesWithSizeAtMost(this Directory dir, int maxSize) {
        var allDirs = dir.DirectoriesRecursive();

        return allDirs
            .Where(d => d.Size <= maxSize);
    }

    private static IEnumerable<Directory> DirectoriesRecursive(this Directory root) {
        return DirectoriesRecursive(root, ImmutableArray<Directory>.Empty);
    }

    private static ImmutableArray<Directory> DirectoriesRecursive(this Directory dir, ImmutableArray<Directory> directories) {
        directories = directories.Add(dir);
        var childDirs = dir.Contents
            .Where(x => x is Directory)
            .Select(x => (Directory) x);
        foreach (var childDir in childDirs) {
            directories = childDir.DirectoriesRecursive(directories);
        }
        return directories;
    }

}
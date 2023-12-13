namespace Lambda.Generic {

    public static class Extensions {

        public static bool Contains(this DirectoryInfo dir1, DirectoryInfo? dir2)
            => dir2?.Exists == true && dir2.Parent != null
                && (dir2.FullName == dir1.FullName || dir2.Parent.FullName == dir1.FullName || dir1.Contains(dir2.Parent));

        public static bool Contains(this DirectoryInfo dir, FileInfo? file)
            => dir.Contains(file?.Directory);
    }
}

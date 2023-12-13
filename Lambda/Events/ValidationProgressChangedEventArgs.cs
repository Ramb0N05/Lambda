namespace Lambda.Events {

    public class FileProgressChangedEventArgs(FileInfo file, int currentNumber, int totalNumber, bool isWritten) : EventArgs {
        public FileInfo CurrentFile { get; } = file;
        public int CurrentFileNumber { get; } = currentNumber;
        public bool IsWritten { get; } = isWritten;
        public int TotalFileCount { get; } = totalNumber;
    }
}

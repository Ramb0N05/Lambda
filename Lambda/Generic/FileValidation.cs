using Lambda.Generic.Enumerations;

namespace Lambda.Generic {

    public class FileValidation(FileInfo file, ValidationResult result) {
        public FileInfo File { get; } = file;
        public ValidationResult Result { get; } = result;
    }
}

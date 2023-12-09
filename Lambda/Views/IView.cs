namespace Lambda.Views {
    public interface IView {
        public abstract string Title { get; set; }

        public abstract DialogResult ShowDialog();
    }
}

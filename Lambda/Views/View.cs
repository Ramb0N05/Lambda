using System.ComponentModel;

namespace Lambda.Views {

    public class AViewDescriptionProvider<TAbstract, TBase> : TypeDescriptionProvider {

        public AViewDescriptionProvider()
            : base(TypeDescriptor.GetProvider(typeof(TAbstract))) {
        }

        public override Type GetReflectionType(Type objectType, object? instance)
            => objectType == typeof(TAbstract)
                ? typeof(TBase)
                : base.GetReflectionType(objectType, instance);

        public override object? CreateInstance(IServiceProvider? provider, Type objectType, Type[]? argTypes, object?[]? args) {
            if (objectType == typeof(TAbstract))
                objectType = typeof(TBase);

            return base.CreateInstance(provider, objectType, argTypes, args);
        }
    }

    public class ViewDesignTime : UserControl {

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Button? AcceptButton { get; set; }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Button? CancelButton { get; set; }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FormBorderStyle FormBorderStyle { get; set; } = FormBorderStyle.FixedToolWindow;

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowInTaskbar { get; set; }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FormStartPosition StartPosition { get; set; } = FormStartPosition.CenterParent;

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string? Title { get; set; }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FormWindowState WindowState { get; set; } = FormWindowState.Normal;
    }

    [TypeDescriptionProvider(typeof(AViewDescriptionProvider<View, ViewDesignTime>))]
    public abstract class View : UserControl, IView {

        public delegate void AcceptButtonClickHandler(object sender, EventArgs e);

        public event EventHandler<EventArgs>? AcceptButtonClick;

        public delegate void CancelButtonClickHandler(object sender, EventArgs e);

        public event EventHandler<EventArgs>? CancelButtonClick;

        public delegate void ViewClosingHandler(object sender, EventArgs e);

        public event EventHandler<EventArgs>? ViewClosing;

        public delegate void ViewClosedHandler(object sender, EventArgs e);

        public event EventHandler<EventArgs>? ViewClosed;

        public const string TITLE_UNDEFINED = "<view.title.undefined>";

        public Button? AcceptButton { get; set; }
        public Button? CancelButton { get; set; }

        public DialogResult DialogResult { get; set; } = DialogResult.None;
        public FormBorderStyle FormBorderStyle { get; set; } = FormBorderStyle.FixedToolWindow;
        public bool ShowInTaskbar { get; set; }
        public FormStartPosition StartPosition { get; set; } = FormStartPosition.CenterParent;
        public string Title { get; set; } = TITLE_UNDEFINED;
        public Form? ViewForm { get; private set; }
        public FormWindowState WindowState { get; set; } = FormWindowState.Normal;

        public void InitializeView() {
            AcceptButtonClick += acceptButtonClick_EventHandler;
            CancelButtonClick += cancelButtonClick_EventHandler;
            ViewClosing += viewClosing_EventHandler;
            ViewClosed += viewClosed_EventHandler;
            ViewForm = createForm();
        }

        private Form createForm() {
            ViewForm = new() {
                AutoSize = AutoSize,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FormBorderStyle = FormBorderStyle,
                MaximumSize = MaximumSize,
                MinimumSize = MinimumSize,
                ShowInTaskbar = ShowInTaskbar,
                Size = Size,
                StartPosition = StartPosition,
                Text = Title,
                WindowState = WindowState
            };

            ViewForm.Controls.Add(this);

            if (AcceptButton is not null)
                AcceptButton.Click += (sender, e) => AcceptButtonClick?.Invoke(sender, e);

            if (CancelButton is not null)
                CancelButton.Click += (sender, e) => CancelButtonClick?.Invoke(sender, e);

            return ViewForm;
        }

        private void acceptButtonClick_EventHandler(object? sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButtonClick_EventHandler(object? sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void viewClosed_EventHandler(object? sender, EventArgs e) {
            //TODO
        }

        private void viewClosing_EventHandler(object? sender, EventArgs e) {
            if (ViewForm is not null)
                ViewForm.DialogResult = DialogResult;
        }

        public virtual void Close() {
            ViewClosing?.Invoke(this, EventArgs.Empty);
            ViewForm?.Close();
            ViewClosed?.Invoke(this, EventArgs.Empty);
        }

        public virtual new void Show() => Show(null);

        public virtual void Show(IWin32Window? owner) => ViewForm?.Show(owner);

        public virtual DialogResult ShowDialog() => ShowDialog(null);

        public virtual DialogResult ShowDialog(IWin32Window? owner) => ViewForm?.ShowDialog(owner) ?? DialogResult.None;
    }
}

namespace OsvitaApp.Helpers.Behaviors;

public class CollectionViewOrientationBehavior : Behavior<CollectionView>
{
    public static readonly BindableProperty ThresholdWidthProperty =
        BindableProperty.Create(nameof(ThresholdWidth), typeof(double), typeof(CollectionViewOrientationBehavior),
            600.0);

    public static readonly BindableProperty SpacingProperty =
        BindableProperty.Create(nameof(Spacing), typeof(double), typeof(CollectionViewOrientationBehavior), 6.0);

    public double ThresholdWidth
    {
        get => (double)GetValue(ThresholdWidthProperty);
        set => SetValue(ThresholdWidthProperty, value);
    }

    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    private CollectionView _collectionView;

    protected override void OnAttachedTo(CollectionView bindable)
    {
        base.OnAttachedTo(bindable);
        _collectionView = bindable;
        _collectionView.SizeChanged += OnSizeChanged;
        UpdateLayout(_collectionView.Width);
    }

    protected override void OnDetachingFrom(CollectionView bindable)
    {
        base.OnDetachingFrom(bindable);
        _collectionView.SizeChanged -= OnSizeChanged;
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
        if (sender is CollectionView collectionView)
        {
            UpdateLayout(collectionView.Width);
        }
    }

    private void UpdateLayout(double width)
    {
        if (_collectionView == null)
            return;

        var newOrientation =
            width > ThresholdWidth ? ItemsLayoutOrientation.Horizontal : ItemsLayoutOrientation.Vertical;

        if (_collectionView.ItemsLayout is LinearItemsLayout layout && layout.Orientation != newOrientation)
        {
            MainThread.BeginInvokeOnMainThread(() => _collectionView.ItemsLayout = new LinearItemsLayout(newOrientation)
            {
                ItemSpacing = Spacing
            });
        }
    }
}
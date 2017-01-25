    public class FloatingMenuItem
    {
        public string Option { get; set; }
        public string ImageSource { get; set; }
        public string Name { get; set; }
        public ICommand Command { get; set; }
    }
    public partial class FloatingMenu : ContentView
    {
        private RelativeLayout mainLayout;
        private StackLayout container;
        private StackLayout backLayer;
        private StackLayout menu;
        private Image menuBtn;
        private bool active;

        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                OnPropertyChanged();
            }
        }

        private ICommand toggleCommand;

        public ICommand ToggleCommand
        {
            get
            {
                return toggleCommand
                    ?? (toggleCommand = new Command(param =>
                    {
                        if (!Active)
                        {
                            menuBtn.Source = ActiveImageSource;
                            Active = true;
                        }
                        else
                        {
                            menuBtn.Source = InactiveImageSource;
                            Active = false;
                        }
                    }));
            }
        }

        public static readonly BindableProperty MainContentProperty = BindableProperty.Create(nameof(MainContent), typeof(DataTemplate), typeof(FloatingMenu), null, BindingMode.OneWay, null, ContentChanged);
        public static readonly BindableProperty InactiveImageSourceProperty = BindableProperty.Create(nameof(InactiveImageSource), typeof(string), typeof(FloatingMenu));
        public static readonly BindableProperty ActiveImageSourceProperty = BindableProperty.Create(nameof(ActiveImageSource), typeof(string), typeof(FloatingMenu));
        public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(string), typeof(FloatingMenu), "Right");
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(FloatingMenu), null, BindingMode.OneWay, null, ItemsSourceCollectionChanged);

        private static void ItemsSourceCollectionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                ((FloatingMenu)bindable).AddMenuItems((IEnumerable<FloatingMenuItem>)newValue);
            }
        }

        public DataTemplate MainContent
        {
            get { return (DataTemplate)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        public string InactiveImageSource
        {
            get { return (string)GetValue(InactiveImageSourceProperty); }
            set { SetValue(InactiveImageSourceProperty, value); }
        }
        public string ActiveImageSource
        {
            get { return (string)GetValue(ActiveImageSourceProperty); }
            set { SetValue(ActiveImageSourceProperty, value); }
        }
        public string Position
        {
            get { return (string)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public FloatingMenu()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private static void ContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FloatingMenu)bindable;

            control.mainLayout = new RelativeLayout();

            control.container = new StackLayout();
            
            control.backLayer = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromRgba(0, 0, 0, 0.5) };
            control.backLayer.SetBinding(IsVisibleProperty, nameof(Active));

            control.menu = new StackLayout();
            control.menu.SetBinding(IsVisibleProperty, nameof(Active));

            control.menuBtn = new Image {WidthRequest = 56, HeightRequest = 56, Source = control.InactiveImageSource, GestureRecognizers = { new TapGestureRecognizer {Command = control.ToggleCommand} }};

            control.container.Children.Add(control.GetContentView());

            control.mainLayout.Children.Add(control.container, 
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent((parent) => parent.Height));

            control.mainLayout.Children.Add(control.backLayer,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent((parent) => parent.Height));

            if (control.Position == "Right")
            {
                control.menu.HorizontalOptions = LayoutOptions.EndAndExpand;
                control.menu.VerticalOptions = LayoutOptions.EndAndExpand;

                control.mainLayout.Children.Add(control.menu,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToView(control.container, (layout, view) => view.Width - 30),
                heightConstraint: Constraint.RelativeToView(control.container, (layout, view) => view.Height - 80));

                control.mainLayout.Children.Add(control.menuBtn,
                   xConstraint: Constraint.RelativeToView(control.container, (layout, view) => view.Width - 70),
                   yConstraint: Constraint.RelativeToView(control.container, (layout, view) => view.Height - 70));
            }
            else
            {
                control.menu.HorizontalOptions = LayoutOptions.StartAndExpand;
                control.menu.VerticalOptions = LayoutOptions.EndAndExpand;

                control.mainLayout.Children.Add(control.menu,
                xConstraint: Constraint.Constant(80),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToView(control.container, (layout, view) => view.Width - 70),
                heightConstraint: Constraint.RelativeToView(control.container, (layout, view) => view.Height - 80));

                control.mainLayout.Children.Add(control.menuBtn,
                   xConstraint: Constraint.RelativeToView(control.container, (layout, view) => 70),
                   yConstraint: Constraint.RelativeToView(control.container, (layout, view) => view.Height - 70));
            }

            control.Content = control.mainLayout;
        }
        
        /// <summary>
        /// Gets view from datatemplate
        /// </summary>
        /// <returns>View</returns>
        private View GetContentView()
        {
            var content = MainContent.CreateContent();

            var view = content as View;

            return view;
        }

        private void AddMenuItems(IEnumerable<FloatingMenuItem> menuItems)
        {
            menu.Children.Clear();
            foreach (var menuItem in menuItems)
            {
                var item = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = Position == "Right" ? LayoutOptions.EndAndExpand : LayoutOptions.StartAndExpand
                };
                if (Position == "Right")
                {
                    item.Children.Add(new Label {Text = menuItem.Name, TextColor = Color.White});
                    item.Children.Add(new Image {Source = menuItem.ImageSource});
                }
                else
                {
                    item.Children.Add(new Image { Source = menuItem.ImageSource });
                    item.Children.Add(new Label { Text = menuItem.Name, TextColor = Color.White });
                }
                item.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = menuItem.Command,
                    CommandParameter = menuItem.Option
                });
                menu.Children.Add(item);
            }
        }
        
    }

# Przykłady ze szkolenia WPF dla zaawansowanych

#  MVVM

## INotifyPropertyChanged

### Implementacja

~~~ csharp
public class Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }

~~~

### Fody


1. Dodaj biblioteki
~~~
PM> Install-Package Fody
PM> Install-Package PropertyChanged.Fody
~~~

2. Utwórz plik FodyWeavers.xml
~~~ xml
<?xml version="1.0" encoding="utf-8" ?>
<Weavers>
  <PropertyChanged/>
</Weavers>
~~~

 Wszystkie klasy, które implementują INotifyPropertyChanged będą miały wstrzyknięty kod do powiadamiania.


https://github.com/Fody/PropertyChanged


## RelayCommand

~~~ csharp
 public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter) => execute?.Invoke();
        public bool CanExecute(object parameter) => canExecute == null || canExecute();        
        
        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        
    }

~~~

# DependencyObject



# Dependency Property
Dependency Property rozszerzają standardowe CLR Property. Dzięki nim dostajemy specyficzne funkcje dostępne tylko w WPF:
- Notyfikacja zmian 
- Walidacja
- Dziedziczenie
- Style
- Data Binding
- Zasoby (Resources)
- Animacje
- Domyślne wartości


## Różnice

- **CLR Property** – odczytuje i zapisuje wartość bezpośrednio z prywatnej zmiennej w klasie. 

- **Dependency Property** – właściwości przechowywane są w słowniku (dictionary) zamiast bezpośrednio w obiekcie. Dostęp do nich możliwy za pomocą metod _GetValue()_ i _SetValue()_

## Własny Dependendency Property

~~~ csharp

 public partial class HeaderUserControl : UserControl
    {

        public HeaderUserControl()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(HeaderUserControl), new PropertyMetadata("", new PropertyChangedCallback(OnCaptionChanged)));

        private static void OnCaptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HeaderUserControl headerUserControl = d as HeaderUserControl;
            headerUserControl.Header.Content = e.NewValue;
        }
    }

~~~ 







# Routed Events

| RoutingStrategy  | Opis                           |
| ---------------- | ------------------------------ |
| Bubbling      | zdarzenie najpierw jest wywoływane w elemencie źródłowym, a następnie podróżuje ono w górę drzewa wizualnego aż do roota             |
| Tunelling         |  zdarzenie wywoływane jest w korzeniu drzewa, a następnie podróżuje w dół drzewa, aż osiągnie element źródłowy   |
| Direct    | zdarzenie jest wywoływane tylko i wyłącznie w elemencie źródłowym – czyli zachowuje się tak samo jak standardowe zdarzenia w .NET  |

## Snippet Routed Event 

1. Utwórz plik _routed-event.snippet_
~~~ xml
<?xml version="1.0" encoding="utf-8" ?>
<CodeSnippets  xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
    <CodeSnippet Format="1.0.0">
        <Header>
            <Title>Define a RoutedEvent</Title>
            <Shortcut>revent</Shortcut>
            <Description>Code snippet for a event using RoutedEvent</Description>
            <Author>Nathan Zaugg</Author>
            <SnippetTypes>
                <SnippetType>Expansion</SnippetType>
            </SnippetTypes>
        </Header>
        <Snippet>
            <Declarations>
                <Literal>
                    <ID>eventhandlertype</ID>
                    <ToolTip>Event Handler Type Type</ToolTip>
                    <Default>RoutedEventHandler</Default>
                </Literal>
                <Literal>
                    <ID>name</ID>
                    <ToolTip>Event Name</ToolTip>
                    <Default>MyEvent</Default>
                </Literal>
                <Literal>
                    <ID>ownerclass</ID>
                    <ToolTip>The owning class of this Property.  
                           Typically the class that it is declared in.</ToolTip>
                    <Default>ownerclass</Default>
                </Literal>
                <Literal>
                    <ID>routingstrategy</ID>
                    <ToolTip>The routing stratigy for this event.</ToolTip>
                    <Default>Bubble</Default>
                </Literal>
            </Declarations>
            <Code Language="csharp">
                <![CDATA[

// Provide CLR accessors for the event
public event RoutedEventHandler $name$
{
        add { AddHandler($name$Event, value); } 
        remove { RemoveHandler($name$Event, value); }
}

// Using a RoutedEvent
public static readonly RoutedEvent $name$Event = EventManager.RegisterRoutedEvent(
    "$name$", RoutingStrategy.$routingstrategy$, typeof($eventhandlertype$), typeof($ownerclass$));

$end$]]>
            </Code>
        </Snippet>
    </CodeSnippet>
</CodeSnippets>
~~~

2. Zaimportuj do Visual Studio za pomocą opcji `Tools | Code Snippets Manager | Import`

3. Użyj wpisując w kodzie C# _revent_


## Attached Events



# Freezable



# Dispatcher

## Invoke

Metoda _Invoke()_ uruchamiania metodę synchronicznie.

~~~ csharp
private void InvokeButton_Click(object sender, RoutedEventArgs e)
{
    Task.Run(() => InvokeMethodExample());
}

private void InvokeMethodExample()
{
    Thread.Sleep(2000);
    Dispatcher.Invoke(() => InvokeButton.Content = "By Invoke");
}
~~~        

## BeginInvoke

Metoda _BeginInvoke()_ uruchamiania metodę asynchronicznie. Nie czeka na jej zakończenie.

~~~ csharp
 private void BeginInvokeButton_Click(object sender, RoutedEventArgs e)
{
    Task.Run(() => BeginInvokeMethodExample());
}

 private void BeginInvokeMethodExample()
{
    Thread.Sleep(2000);

    DispatcherOperation op = Dispatcher.BeginInvoke((Action)(() => BeginInvokeButton.Content = "By BeginInvoke"));
~~~        
uwaga: kompilator nie zna typu delegata dlatego trzeba rzutować


## DispatcherObject


## Dispatcher Timer

~~~ csharp
  DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += (t, s) => DoWork();
            timer.Start();
~~~


# Klasy WPF

- **Object** - klasa bazowa dla wszystkich klas .NET
- **DispatcherObject** - klasa bazowa dla dowolnego obiektu, który chce być dostępny tylko w wątku, który go utworzył. Dispatcher utrzymuje priorytetową kolejkę elementów pracy dla określonego wątku. Większość klas WPF wywodzi się z DispatcherObject i dlatego z natury jest niebezpieczna dla wątków.
- **DependencyObject** - klasa bazowa, która zapewnia obsługę Dependency Properties. Definiuje metody _GetValue_ i _SetValue_
- **Freezable** - klasa bazowa dla obiektów, które mogą być "zamrożone" do stanu tylko-do-odczytu ze względu na wydajność. Takie obiekty, po zamrożoneniu, mogą być bezpiecznie współdzielone przez wiele wątków, w przeciwieństwie do DispatcherObjects. Zamrożone obiekty nigdy nie mogą zostać odmrożone, ale sklonowane do niezamrożonych kopii.
- **Visual** - klasa bazowa abstrakcyjna, dla wszystkich obiektów, które mają własną reprezentację wizualną. Jego główną rolą jest zapewnienie wsparcia renderowania.
- **UIElement** - klasa podstawowa dla wszystkich obiektów wizualnych z obsługą Routed Events, Command Binding, Layout i fokus.
- **ContentElement** - klasa bazowa podobna do UIElement, ale dla fragmentów treści, które nie mają własnego zachowania renderowania. Zamiast tego obiekty ContentElements są hostowane w klasie pochodnej Visual, która ma być renderowana na ekranie.
- **FrameworkElement** - klasa bazowa, która dodaje obsługę stylów, powiązań danych, zasobów i kilku typowych mechanizmów do sterowania opartego na systemie Windows, takich jak podpowiedzi i menu kontekstowe.
- **Shape** - klasa bazowa dla kształtów takich jak Ellipse, Polygon i Rectangle.
- **Panel** - klasa bazowa do pozycjonowania elementów 
- **Control** - klasa bazowa, która dodaje do FrameworkElement właściwości Foreground, Background, FontSize i Template
- **ContentControl** - klasa bazowa dla kontrolek, które mogą mieć tylko jeden element podrzędny. Element podrzędny może być dowolnym elementem od ciągu znaków do panelu układu z kombinacją innych elementów sterujących i kształtów.
- **ItemsControl** - klasa bazowa dla kontrolek, których można użyć do prezentacji kolekcji elementów, takich jak elementy ListBox i TreeView.

# Hierarchia klas

| Nazwa                | Threading | DP | Rendering | Hit Testing | Layout | Input | Focus | Events | Styles | Data Binding | Resources | Animation | Template |
| -------------------- | ----------|----|---------- | ----------- | ------ | ----- | ----- | ------ | ------ | ------------ |  -------- | -------- | -------- | 
| DispatcherObject     | x |   |
| DependendencyObject  | x | x | 
| Visual               | x | x | x | x |
| UIElement            | x | x | x | x | x | x | x | x | 
| FrameworkElement     | x | x | x | x | x | x | x | x | x | x | x | x | 
| Control     | x | x | x | x | x | x | x | x | x | x | x | x | x |
| ContentControl     | x | x | x | x | x | x | x | x | x | x | x | x | x |
| ItemsControl     | x | x | x | x | x | x | x | x | x | x | x | x | x |


# Resources


## Zasoby statyczne i dynamiczne
- **Statyczne** - {StaticResource} zasób jest przypisany tylko raz, w momencie gdy pierwszy raz jest potrzebny.
- **Dymaniczne** - {DynamicResource} zasób jest przypisywany za każdym razem, gdy zostanie zmieniony.

## Współdzielenie zasobów
Standardowo element umieszczony w zasobach jest współdzielony przez wiele obiektów. Można zmienić to zachowanie poprzez atrybut _x:Shared="false"_

Wskazówka: Zasoby statyczne są mniej zasobożerne, gdyż nie wymagają ciągłego śledzenia zmian.

~~~ xml
<Window.Resources>        
        <Button x:Key="MyButton" x:Shared="false" Background="Red" Content="Test" />
</Window.Resources>

<Grid>
 <ListBox>
        <ListBoxItem Content="{StaticResource MyButton}" />
        <ListBoxItem Content="{StaticResource MyButton}" />
        <ListBoxItem Content="{StaticResource MyButton}" />
 </ListBox>
</Grid>
~~~

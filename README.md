# NKit

# CallMethodAction

`MethodName`: 指定要调用的方法的名称。

`TargetObject`：指定MethodName所属的对象。默认是CallMethodAction的宿主 — 逻辑树上的依赖对象。

`PassTriggerArgsToMethod`: 指明Trigger传递过来的参数是否作为`MethodName`的参数，默认值是false。CallMethodAction放置到不同的Trigger中，传递过来的参数可能不同。Microsoft.Xaml.Behaviors内置的PropertyChangedTrigger传递的参数是PropertyChangedEventArgs; EventTrigger是RoutedEventArgs或其派生类，比如MouseUp，参数是MouseButtonEventArgs。

`TriggerArgsConverter`: 对Trigger传递过来的参数进行转换。`PassTriggerArgsToMethod = True`时有效。

`TriggerArgsConverterParameter`: `TriggerArgsConverter`的第3个参数的实参。`PassTriggerArgsToMethod=True`时有效。

`Parameters`: 当`PassTriggerArgsToMethod`是false时，`Parameters`会作为`MethodName`的参数。注意：MethodName的参数列表必须与`Parameters`数量，类型，顺序完全匹配，否则抛出异常`Missed method xxx`。

> Sample1：实时追踪记录，鼠标在按钮中的位置。

```xml
<Button Name="Button" Content="实时追踪鼠标的位置">
    <i:Interaction.Triggers>
        <i:EventTrigger EventName="MouseMove">
            <local:CallMethodAction MethodName="RecordMousePoint"
                                    PassTriggerArgsToMethod="True"
                                    TargetObject="{Binding}"
                                    TriggerArgsConverterParameter="{x:Reference Button}">
                <local:CallMethodAction.TriggerArgsConverter>
                    <local:MouseEventArgsConverter />
                </local:CallMethodAction.TriggerArgsConverter>
            </local:CallMethodAction>
        </i:EventTrigger>
    </i:Interaction.Triggers>
</Button>
```

```c#
public class ViewModel
{
    public void RecordMousePoint(Point point)
    {
        Console.WriteLine(point.X + "," + point.Y);
    }
}

public class MouseEventArgsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        MouseEventArgs args = (MouseEventArgs)value;
        return args.GetPosition(parameter as UIElement);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

> Sample2：关闭窗口

```xml
<Button Content="关闭窗口">
    <i:Interaction.Triggers>
        <i:EventTrigger EventName="Click">
            <local:CallMethodAction MethodName="Close" TargetObject="{Binding RelativeSource={RelativeSource AncestorType=Window}}" />
        </i:EventTrigger>
    </i:Interaction.Triggers>
</Button>
```

> Sample3：调用ViewModel中的PrintSum()并指定参数

```xml
<Button Content="PrintSum">
    <i:Interaction.Triggers>
        <i:EventTrigger EventName="Click">
            <local:CallMethodAction MethodName="PrintSum" TargetObject="{Binding}">
                <local:Parameter>
                    <local:Parameter.Value>
                        <system:Int32>1</system:Int32>
                    </local:Parameter.Value>
                </local:Parameter>
                <local:Parameter>
                    <local:Parameter.Value>
                        <system:Int32>2</system:Int32>
                    </local:Parameter.Value>
                </local:Parameter>
            </local:CallMethodAction>
        </i:EventTrigger>
    </i:Interaction.Triggers>
</Button>
```

```c#
public class ViewModel
{
    public void PrintSum(int a, int b)
    {
        Console.WriteLine(a + b);
    }
}
```


# Floating Menu Button For Xamarin Forms
Code for a floating menu button for Xamarin Forms similar to Material Design one

###Setup
*No extra setup needed

###Usage

```xaml
<controls:FloatingMenu x:Name="FloatingMenuControl" ActiveImageSource="ImageWhenPressed" InactiveImageSource="ImageWhenNotPressed" Position="Right">
    <controls:FloatingMenu.MainContent>
      <DataTemplate>
        <!--Your content here-->
        <ScrollView HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">
        <StackLayout HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">
          <Label Text="Hola" />
        </StackLayout>
        </ScrollView>
        <!--Your content here-->
      </DataTemplate>
    </local:FloatingMenu.MainContent>
  </local:FloatingMenu>
```
The MIT License (MIT)

Copyright (c) 2016 Alset & Tristan Martinez

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

   M S B A M L   `   `   `  ���� H  DJournal-IO-v2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Journal_IO_v2Journal_IO_v2      Journal_IO_v2.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F BNHotkey.Wpf, Version=1.2.1.0, Culture=neutral, PublicKeyToken=nullR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:Journal_IO_v2     -     �Name$  Window_1�� � Title$ 
MainWindow��$��802.515��$��1355.639��.��]� .+�� $��0,0,2.667,10.333q� J� IsManipulationEnabled .  � ColumnDefinitions �� $
��139* ��� $��0* ��� $��1207* ��� $��0* �.+��� -    ��Name$ 	dateEntry��$
��Left=�$��	25,45,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$
��Left=�$��21��$	��104���� -   $ NewEntryBox��$��Stretch=�$
��auto��$��2.667,23,10,466q�$
 Wrap=�$��Stretch=�$
��auto��$��2�� �� 	IsEnabled . �� -   $ Output��$��Stretch=�$
��auto��$
 Wrap=�$��Stretch=�$
��auto��$��2.667,302,10,0q�$��2�� �� 
IsReadOnly . " �� VerticalScrollBarVisibility$ Visible=��� -   	 ��Name$	 AddEntry��$	��Add��$
��Left=�$��	54,71,0,0q�$	��Top=�$��75��$��20��$
��Left=�c� $
��Left=�$��16��$	��Top=�$��53��$
��File��.@��� -   $	 
SaveButton��$��Save (Control + S)���� -   $	 SaveAsButton��$#��Save As (Control + Shift + S)���� -   $	 
OpenButton��$��Open (Control + O)���� -	   $	 	NewButton��$��New (Control + N)���� -
   $	 SearchButton��$��Search (Control +F)��
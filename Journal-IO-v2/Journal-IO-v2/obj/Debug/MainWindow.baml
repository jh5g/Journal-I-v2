   M S B A M L   `   `   `  ����H  DJournal-IO-v2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Journal_IO_v2Journal_IO_v2  5   	       Journal_IO_v2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Journal_IO_v2     5   	   -   5          �Name$  Window_1��5   	    � Title$ 
MainWindow��5   	   $��635.551��6   $��1024.615��6-    J� InputBindings 5	      �� 5
   
    �� 	Modifiers$
 Ctrlf�6   
 �� Key$ S��6&    �� Command 6.   �� *SaveCommand+�� 5   
   $
 Ctrlf�6   $ O��6&    6.   �� *OpenCommand+�� 5   
   $
 Ctrlf�6   $ N��6&    6.   �� *
NewCommand+�� 5   
   $ 
Ctrl+Shiftf�6   $ S��6,    64   �� *SaveAsCommand+.��5      �  � ColumnDefinitions 5   
   �� 5      $	��46* �6   �� 5      $
��351* �6   .+�5   
   �� -   5         ��Name$ 	dateEntry��5      $
��Left=�6%   $��23��6@   $��	10,23,0,0q�6L    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $	��120��6�   $��2��6�   �� 5   
   -   5        $ NewEntryBox��5      $
��Left=�6'   $	��230��6B   $��	43,23,0,0q�6O   $
 Wrap=�6b   $	��Top=�6v   $	��818��6�   $��1��6�   	 �� 	IsEnabled	 . 6�   �� 5   
   -   5        $ Output��5      $
��Left=�6"   $	��331��6=   $
 Wrap=�6J   $	��Top=�6^   $	��818��6v   $��
43,265,0,0q�6�   $��1��6�   
 �� 
IsReadOnly
 . " �� VerticalScrollBarVisibility$ Visible=�6�   �� 5   
   -   5         ��Name$ AddEntry��5      $	��Add��6#   $
��Left=�61   $��	55,59,0,0q�6L   $	��Top=�6_   $��75��6w   $��2��6�   c� 5   
   $
��Left=�6   $��16��6-   $	��Top=�69   $��53��6Q   $
��File��6\   .@�5      �� -   5        $ 
SaveButton��5      $��Save (Control + S)��6)   ��6F   �� *SaveCommand+6f   �� 5      -   5        $ SaveAsButton��5      $#��Save As (Control + Shift + S)��6+   ��6S   �� *SaveAsCommand+6u   �� 5      -   5        $ 
OpenButton��5      $��Open (Control + O)��6)   ��6F   �� *OpenCommand+6f   �� 5      -	   5        $ 	NewButton��5      $��New (Control + N)��6(   ��6D   �� *
NewCommand+6c   5      5      5       
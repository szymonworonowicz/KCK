   M S B A M L   `   `   `  ����H  DKCKProjektWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:KCKProjektWPF.PagesKCKProjektWPF.Pages  5      !    KCKProjektWPF.Pages.AboutPg   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:KCKProjektWPF.Pages     5        N� Title$  AboutPg��5	       N� 
Background
 �   � N� 
FontFamily$ Italic)�6*    N� FontSize$ 24(�6>   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $
��auto �6   6.   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   .+�5   
   � $��1��6   $��1��6    5      �� 5      6%    5      �� 5      6"   �� 5      6"   �� 5      6"   �� 5      $	��10* �6    60   �� 5      6#   .+�5!      �� $��Center=�6   $��Center=�6/   $��5q�6L   $��50,10,50,10q�6W   
���   �
���   �
�������.��6�   Szymon Woronowicz6�   �� 5"      $��1��6   $��Center=�6!   $��Center=�6<   $��5q�6Y   $��50,10,50,10q�6d   
���   �
���   �
�������.��6�   Julia Gejdel6�   �� 5#      $��2��6   $��Center=�6!   $��Center=�6<   $��5q�6Y   $��50,10,50,10q�6d   
���   �
���   �
�������.��6�   Pawel Krzywosz6�   �� 5$      -   5        $��0��5$   (   $��4��68   $��2��6E   $��5,30,5,5q�6Y   $��Center=�6k   $��Center=�6�   $��50,10,50,10q�6�   
���   �
�������
���   �$��24(�6�   $��Italic)�6  $
��Exit��6  z�5%      �� 5&      -   5        Scaleb1�� �� ScaleX$
 0.96Q�5&   6    �� ScaleY$
 1.06Q�6D   6T    � Triggers 5(      :� 5)      	 :� RoutedEvent$	 Image.Loaded��6#   .[�5*      �� .��5+      �� .��5,   "   W� 
 �� 
TargetName$
 Scaleb1��5-   $    �� TargetProperty$ ScaleX�5.   %    W� From 0.965/   %   	 W� To 1.0661    g� Duration$ 0:0:0.5D�6;    g� AutoReverse$
 True��50   %    g� RepeatBehavior$ Forever��68   W� 51   "   $
 Scaleb1��52   $   $ ScaleY�53   %    0.9654   %    1.0661   $ 0:0:0.5D�6;   $
 True��55   %   $ Forever��68   56      57      58      5:      5;      5<      5=      
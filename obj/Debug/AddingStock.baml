   M S B A M L   `   `   `  ����D  @DashBoard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DashBoard	DashBoard  5   	       DashBoard.AddingStock   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:DashBoard     5   	     � WindowState$  	Maximized=�5   	    � Title$ AddingStock��5	   	   $	��450��6   $	��800��6*   .��5
      �� .��5   
   �  � RowDefinitions 5      �� 5      $	��200 �6    6/   �� 5      6"   �� 5      $	��200 �6    6/   .+�5      �� $��0��6   $��Center=�6&   $��Center=�6A   .+�5      ��  �� Padding$ 20q�6   $��35(�6)   $��	Agency FB)�67   $
��Bold"�6N   Adding Stock6n   �� 5      -   5         ��Name$ previous��5      $ 20q�6.   $��20(�6;   Previous PO:6Y   5      � 5      -   5         �Name$
 root��5      $��20q�6!   $��1��6-   $	��Top=�6:   $��Center=�6R   5      �� 5      $��2��6    �� Orientation$ 
Horizontal=�6&   $��Center=�6?   $��Center=�6\   .+�5      �� $
��20,0q�6   $ 
Horizontal=�6+   $��Center=�6D   $��Center=�6a   .+�5      �� $��20(�6    $ 20q�6.   $	��200��6;   Enter Barcode6V   �� 5      -   5         ��Name$ 	BarcodeID��5   3   $��20(�6F   $��5q�6T   $	��200��6`   $��40��6l   6z   5      �� 5      $
��20,0q�6   $ 
Horizontal=�6+   $��Center=�6D   $��Center=�6a   .+�5      �� -   5        $
��20,5q�5   0   $��20(�6?   
�������$
��20,0q�6d   .��6r   Genarate PO6   �� 5      -   5        $
��20,5q�5   +   $��20(�6:   
�������$
��20,0q�6_   .��6m   Clear6t   5       5!      5"      5#      5$      
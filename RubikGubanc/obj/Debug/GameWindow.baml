   M S B A M L   `   `   `  ����F  BRubikGubanc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RubikGubancRubikGubanc  5   	       RubikGubanc.GameWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'bclr-namespace:RubikGubanc     5   	     � Title$  	A játék��5   	   $	��450��6   $	��600��6&    � WindowStartupLocation$ CenterScreen=�62    � 	Resources 5      &    RubikGubanc.BoolToColorConverter   BoolToColor  SecondDimension � LayoutTransform �� CenterX �� CenterY �� Angle  FirstDimension �� Orientation��%�  &	        &	      &	 �       5   
   6!   �� 5   
   .R�5	      �� $	��100��6,   $	��100��69   ��6   �� *URL+ 5
      �� 5      $	 0.5Q�6&   $	 0.5Q�64    6B   �� *
Rotation+5      5      �� 5   
   .R�5      �� #���  63   ��6   �� ��5      �� 5      .R�5      �� $ 
Horizontal=�6%   5      5      5      5      .��5      � ��6   �� *Solved+ �� 	Converter# [  .+�5   
   � $��60,0,0,0q�6   $��Center=�6!   $
��Left=�6<   $	��300��6W   .+�5      �� #���  6>   ��6   �� *
ImageOrder+6>   5      �� 5   
   -   5        $��	Megoldás��5      $��Right=�6$   $��	0,10,10,0q�6@   $	��Top=�6S   $	��160��6k   $��35��6w   $��20(�6�   $��Algerian)�6�   6�   �� 5   
   -   5        $��Keverés��5      $��Right=�6#   $��	0,60,10,0q�6?   $	��Top=�6R   $	��160��6j   $��35��6v   $��20(�6�   $��Algerian)�6�   6�   �� 5   
   -   5        $��Leírás��5      $��Right=�6"   $��	0,0,10,60q�6>   $��Bottom=�6Q   $	��160��6l   $��35��6x   $��20(�6�   $��Algerian)�6�   6�   �� 5    
   -   5        $��	Kilépés��5       $��Right=�6#   $��	0,0,10,10q�6?   $��Bottom=�6R   $	��160��6m   $��35��6y   $��20(�6�   $��Algerian)�6�   	 �� IsCancel	 . 6�   5!      5"      
   M S B A M L   `   `   `  ����M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 (  $BackstageToggleButtonControlTemplate  �� 
TargetType    Fluent.Backstage ��Name ��Name ��Name �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName	 �� Property
 �� Value P� Property P� Value   Fluent.GrayscaleEffect �� EndPoint �� 
StartPoint � Color � Offset �� 
SourceName  RibbonBackstageStyle �� 
TargetType �� RelativeSource �� AncestorType   Fluent.MetroWindow  DefaultFontFamily  DefaultFontSize   Fluent.FrameworkHelper �� BasedOn�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      Fluentclr-namespace:Fluent  5      8d2http://schemas.microsoft.com/expression/blend/2008  6)   Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  6f   %�  &	        &	 �	    7   '   S    7  �� 5      "    6C   .R�5      �� $ border2��6   $��56��6   $��24��6,   #��z��68   $��0,0,0,0q�6b   ._�5      �� $ border1��6   ._�5   
   �� $ border��6   ._�5	      � .+�5
      �� $ 	rectangle��6   �� 5      $ contentPresenter��6   $��Header��69   $��Center=�6P   $��Center=�6k   5      5      5   	   5       5      ]� 5       5   	   �� 5   
    �@J�IsMouseOver . 6+   ./�5   	   �� $ 	rectangle��6    	 � ��
 �	#3FFFFFFF67   5   	   P� 5       � {� .  . �5   	   �� $ border2��6#   	 �@J�Opacity$	
 0.5Q�68   �� 5   	   $ contentPresenter��6"   	 �@J�Effect
 5   
     5      5   
   5   	   P� 5       �@  IsOpen . . �5    	   �� $ border2��6"   	 � ��$
 0q�67   �� 5!   	   $ 	rectangle��6&   	 � y�$
 	Collapsed=�6=   �� 5"   	   $ border1��6'   	 � ��
 5#   
   �� 5$      $ 0.5,1&�6   $ 0.5,0&�60   .��5%      � $ 	#99959595��6   $ 0Q�6+   � 5&      $ 	#7F909090��6   $ 1Q�6+   5'      5)   
   �� 5*   	   $ border��6'   	 � ��
 5+   
   �� 5,      $ 0.5,1&�6   $ 0.5,0&�60   .��5-      � $ 	#4CFFFFFF��6   $ 0Q�6+   � 5.      $ 	#72FFFFFF��6   $ 1Q�6+   5/      51   
   �� 52   	   $ border��6&   	 � ��
 53   
   �� 54      $ 0.5,1&�6   $ 0.5,0&�60   .��55      � $ 	#19FFFFFF��6   $ 0Q�6+   � 56      $ 	#66FFFFFF��6   $ 1Q�6+   � 57      $	 0.5Q�6   $ 	#3FFFFFFF��6&   58      5:   
   5;   	   ]� 5<       5=   	   �� 5>   
   $ border2��68    �@J�IsMouseOver . �� 5?   
    �@  IsOpen . 6&   ./�5A   	   �� $ 	rectangle��6&   	 � y�$
 	Collapsed=�6=   �� 5B   	   $ border1��6'   	 � ��
 5C   
   �� 5D      $ 0.5,1&�6   $ 0.5,0&�60   .��5E      � $ 	#A0A1A1A1��6   $ 0Q�6+   � 5F      $ 	#879A9A9A��6   $ 1Q�6+   5G      5I   
   �� 5J   	   $ border��6'   	 � ��
 5K   
   �� 5L      $ 0.5,1&�6   $ 0.5,0&�60   .��5M      � $ 	#59FFFFFF��6   $ 0Q�6+   � 5N      $ 	#7FFFFFFF��6   $ 1Q�6+   5O      5Q   
   �� 5R   	   $ border��6&   	 � ��
 5S   
   �� 5T      $ 0.5,1&�6   $ 0.5,0&�60   .��5U      � $ 	#26FFFFFF��6   $ 0Q�6+   � 5V      $ 	#7FFFFFFF��6   $ 1Q�6+   � 5W      $	 0.5Q�6   $ 	#4CFFFFFF��6&   5X      5Z   
   5[   	   5]      �� 5_      "   5`      .�5a   
   �� 	 � ��8
   6%   �� 5b   
   	 � ��
 6'   �� *RibbonThemeColor+ �� *FindAncestor+"  �� 5c   
   	 � ��#
 �  6'   �� 5d   
   	 � ��#
 �  6%   �� 5e   
   	 � ��

 �����6'   �� 5f   
   	 �@J�SnapsToDevicePixels
 . 60   �� 5g   
   	 �@ UseLayoutRounding
 . 6E   5h      �� 5j      "   6   8   63   5k      
   M S B A M L   `   `   `  ���� I  EControlLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ControlLibraryControlLibrary  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089b ^Microsoft.Windows.Shell, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35  �� MergedDictionaries �� Source!    ControlLibrary.ModernWindow �� 
TargetType �� Property   WindowBackgroundContent �� Value  DefaultFontFamily  DefaultFontSize  
WindowText&   System.Windows.Media.TextOptions �� 
TargetType ��Name/  )Microsoft.Windows.Shell.SystemParameters2   Current �� Source	 �� Path  WindowBackgroundColor
 �� Color � 	Resources  Key �� 
TargetName �� TargetProperty	 �� To g� Duration  WindowBorder �Name � RowDefinitions    BackgroundContent 
 Accent    LogoData �� Stretch �� Orientation*  $Microsoft.Windows.Shell.WindowChrome   IsHitTestVisibleInChrome � ToolTip  SystemButton,  &Microsoft.Windows.Shell.SystemCommands   MinimizeWindowCommand �� RelativeSource �� Mode �� AncestorType ��Name   RestoreWindowCommand � UseLayoutRounding   MaximizeWindowCommand    CloseWindowCommand!    
TitleLinks" ��Name	# �� X1	$ �� Y1	% �� X2	& �� Y2 	 SeparatorBackground 
 SystemButtonLink' �� ElementName( �� Triggers) �� Binding* �� Value+ �� 
TargetName   ControlLibrary.ModernMenu,    MenuLinkGroups-   
LinkGroups.   SelectedSource/ �� Mode!   ControlLibrary.ModernButton0   IconData    ControlLibrary.ModernFrame1  Name2    ContentLoader3   ContentLoader4   Source5 F�Name  WindowBorderActive   BooleanToVisibilityConverter6 �� 	Converter7 �� Triggers8 P� Property9 P� Value: ]� 
Conditions; �� Property< �� Value=   CornerRadius>   GlassFrameThickness?   UseAeroCaptionButtons�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     )controlsclr-namespace:ControlLibrary  Kshell?http://schemas.microsoft.com/winfx/2006/xaml/presentation/shell    �� $2 ,/ControlLibrary;component/Assets/Button.xaml?��� $6 0/ControlLibrary;component/Assets/Converters.xaml?�%�  '         7  7  7  7  7 
 7  �� "   .���  �@  BackgroundContent# �   ��  � ��# �  ��  � ��# �  ��  � ��# �  ��  � ��$	 800����  � ��$	 640����  � ��$	 320����  � ��$	 320����  �@�
ResizeMode$ CanResizeWithGrip=���  �@�UseLayoutRounding . ��  �@ TextFormattingMode$ Display=���  � �� �� "   .R��� $ WindowBorder������ # Z $#	 WindowNonClientFrameThickness����� WindowBorderBackground��#
 �   ���� BackgroundAnimation .���� $ WindowBorderBackground��$ Color�# �  $ 0:0:.6D�._�� .+��� #���  $��1q�._��� .u�� $ 
LayoutRoot�� �� $��36 ��� $
��Auto ��� $
��Auto ��� $��* ��� $��15 �.+��� $��5��#��z �� #���  $��5��$z�1,0,0,1,0,32W�$��14��$��53��$
��Left=�$	��Top=��� #���  $��36��$��36��$��Right=�$	��Top=�$��0,0,76,0q�._�F� #��z $
 Fill=�
�������$��24��$��24��$��Center=�$��Center=��� $ 
Horizontal=�$��Right=�$	��Top=�$��0,8,6,0q� . .+��� $ minimize��8��  ���� # Z ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� ����    ��[ � z ��[ 	$��8��$��7��$��Center=�$��Center=�$��2������ *
Foreground+ �� $ FindAncestor=�$ ButtonL�� $��1,0,1,0q�.+��� $ Restore��$ restore��8�� $y�	Collapsed=����� # Z ��� $��13��$��12�� . $z�1,0,0,1,.5,.5W�.+�F� X���Ѐ�     � z     � z ��[ �    ��- р�[ ��- �    �� р�[ �� р�[  z �     z 	$��8��$��8��$��Center=�$��Center=�$��1������ *
Foreground+ �� $ FindAncestor=�$ ButtonL��� $ Maximize��$ maximize��8�� ���� # Z ��� $��13��$��12��.+�F� +����    @B �@T� @B �@T�  z �     z 	$��9��$��8��$��Center=�$��Center=�$��2������ *
Foreground+ �� $ FindAncestor=�$ ButtonL��� $ close��8�� ���� # Z  ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� +����        � z ��j � z     �    ��j 	$��8��$��7��$��Center=�$��Center=�$	��1.5������ *
Foreground+ �� $ FindAncestor=�$ ButtonL��� #��z! $��Right=�$	��Top=�$��	0,6,180,0q� . ���� .R��� $ 
Horizontal=����� .R��� $ 
Horizontal=�.+��� $" 	Separator��$# .5��$$ 3��$% .5��$& 12��$��7,0,5,0q�#��� 	 �� $!��NavigationCommands.GoToPage��8�� ���� *DisplayName+���� *Source+���� $' ContentFrame��( �� ) ��  �� *PreviousData+* Z� .a��� $+ 	Separator�� � y�$ 	Collapsed=�  $��1��$��36,0,0,0q�#- z, . �� *Source+$' ContentFrame��$/ TwoWay=�  a0 ��@����O�@������p\-�������@��� �8@����ا  6n� �8��� ������� �p\-��O�@����O$#��NavigationCommands.BrowseBack��$
 back��$��2��$
��Left=�$	��Top=�$��	34,24,0,0q� . ���� $' ContentFrame��  $1 ContentFrame��$��3��$��2��$��
36,8,16,16q�#3 z2 4 �� *ContentSource+ �� *TemplatedParent+$/ TwoWay=�� $��4��.+�F� $5 
ResizeGrip��$y�	Collapsed=�$��12��$��12��$��1q�$��Right=�#���  $��1��$
 None=������@B ��� ���- ��� �@KL ��� ���j ��� �@T� ��� ��ا ��� Ѐ� @T� р� �ا Ѐ�[ @T� р�[ �ا Ѐ�� @T� р�� �ا �@KL ��[ ���j ��[ �@T� ��[ ��ا ��[ Ѐ�[ @KL р�[ ��j Ѐ�� @KL р�� ��j �@T� �� ��ا �� Ѐ�� @B р�� ��- �� #���  $��1q�y��� *
IsActive+ �� *FindAncestor+" �86  7 P� 8 �@�WindowState$9 	Maximized=�. ��� $+ Maximize�� � y�$ 	Collapsed=��� $+ Restore�� � y�$ Visible=��� $+ 
LayoutRoot�� � ��$ 7q�P� 8 �@�WindowState$9 Normal=�. ��� $+ Maximize�� � y�$ Visible=��� $+ Restore�� � y�$ 	Collapsed=�]� : �� ; �@�
ResizeMode$< CanResizeWithGrip=��� ; �@�WindowState$< Normal=�./��� $+ 
ResizeGrip�� � y�$ Visible=���  �@ WindowChrome   $= 0��$> 1q�? .  
   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  L HCMS.WPFHeadOffice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CMS.WPFHeadOfficeCMS.WPFHeadOffice O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem I EControlLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ControlLibrary;assembly=ControlLibraryControlLibrary "   CMS.WPFHeadOffice.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     7Fluent$clr-namespace:Fluent;assembly=Fluent     6
FluentTestclr-namespace:CMS.WPFHeadOffice     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9System&clr-namespace:System;assembly=mscorlib     K
CMSControl4clr-namespace:ControlLibrary;assembly=ControlLibrary     -      Fluent.MetroWindow   Name$  window�� � Title$) #Chain management system back office��$	��600��$	��800�� � 
ResizeMode$ CanResizeWithGrip=�   RibbonThemeColor$ 	#FF217346��$��LeftToRight=�
������� � 	Resources  �� MergedDictionaries �� Source��  �� $/ )/Fluent;Component/Themes/Metro/White.xaml?�%     J� CommandBindings .���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+�   Fluent.Ribbon  -   	  Name$	 ribbon��
   AutomaticStateManagement
 .    ContextualGroups    Menu    Fluent.Backstage     Content.     Fluent.BackstageTabControl  .@�   Fluent.Button  -      Header$
 Save��   Icon$ Images\Save.png��   Fluent.KeyTip   Keys$ S��  $ Save As��$ Images\Save.png��$ A��   Fluent.BackstageTabItem     Header$
 TEST��$��Hello������ .R�� .+��� ����  �� Mode$ OneWay=�   Fluent.SeparatorTabItem  $��	Separator��  $
 Info��$ E��{�.  .���� .��� $	��400��
���  ��.+��� $	��100��$	��100��$	��Top=�$
��Left=�.@��� .��1�� .��2�� .��3�� .��4�� .��5�� .��6�� .��7�� .��8�� .��9�� .��10	   Fluent.ComboBox	   �� SelectedIndex$ 1��.@��� .��1�� .��2�� .��3�� .��4�� .��5  $ Recent��$ R��.���� $��	20,5,20,5q� !  InnerBackstageTabControlStyle#���   .@�  $��Test1���� $��1���� $��2���� $��3��  $��Test2���� $��4��    $	 New��$ N��.��� $��50q�$��0��$
��Auto��$��Stretch=�.+��� $
��Left=�$��0��$
��Wrap=�
���   ���This is long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long long text  -      IsDefinitive . {�.   ButtonBackstageStyle#���  $
��Left=�$ Images\Paste.png��   	LargeIcon$ Images\PasteLarge.png��. 123
   Fluent.DropDownButton
   
  Header$	 123��$��Right=� 
  Icon$ Images\Paste.png�� 
  	LargeIcon$ Images\PasteLarge.png��	  $��Bottom=�.@��� $��1�� # MetroComboBoxItemBackstageStyle#���  �� $��2���� $��3���� $��4���� $��5��   Fluent.Spinner  -      Value$ 1Q�   Format$ 0��   
InputWidth$ 50Q�   Fluent.RibbonControl   Header$
 lala��$��Right=�$��Bottom=�  $ Print��$ P��.��� 
��� ��� J� Opacity$	 0.3Q�$��50q�  $
 Exit��$ Images\Exit.png��$ X��$��ApplicationCommands.Close��   ToolBarItems   -      Size$ Small=�$ Images/Help.png��  � ToolTip     Fluent.ScreenTip  !   Title$! Insert Chart��"   Text$y" sInsert a chart to illustarate and compare data. 

Bar, Pie, Line, Area and Surface are same of the available types.��#   Image$[# Upack://application:,,,/CMS.WPFHeadOffice;component\Images\SampleImageForScreenTip.png��$   	HelpTopic$ $ http:\\fluent.codeplex.com��%   DisableReason$% Disable reason reason.��$	��190��&   IsRibbonAligned& . '   QuickAccessItems'     Fluent.QuickAccessMenuItem  ( m� 	IsChecked( . )   Target) �� * �� ElementName$* pasteButton��  ( . ) �� $* 	cutButton��  ) �� $* 
copyButton��+   Tabs.+    Fluent.RibbonTabItem  -   ,  Name$, homeTabItem��-   Header$- 	Dashboard��$ H��$  Regular Tool Tip��.   ReduceOrder$Q. KFont, Font, Font, Font,B,A,A,A,(A),(A),(A),Clipboard,Font,B,B,(A),C,(A),(A)��/   Groups./    Fluent.RibbonGroupBox  -   0  Name$0 	Clipboard��1   Icon$K1 Epack://application:,,,/CMS.WPFHeadOffice;component/Images/Default.png��$ C��2   Header$2 	Clipboard��3   IsLauncherVisible3 . 4   LauncherKeys$4 ZB��.@�   Fluent.SplitButton  -	   5  Name$5 pasteButton��$ V��$ Paste��$ Images\Paste.png��$ Images\PasteLarge.png��6 
  SizeDefinition$6 Large��7   Command$7 Paste��    $! Paste (Ctrl+V)��$*" $Paste the contents of the Clipboard.��$$% Enable it do use this function��$	��190��.@�   Fluent.Gallery  $��Hidden=�8   Orientation$8 
Horizontal=�9   GroupBy$	9 Tag��:   
Selectable: .  ;   	ItemWidth$; 32Q�<   
ItemHeight$< 32Q����� = �� 
TargetType   Fluent.GalleryItem"=  > �� BasedOn#> [B .��� ? �� Property? �@ Command@ �� Value$@ Paste��.@��� $��Images\PasteImage.png��$��24��$��24��$ K��
A � Tag$A Paste Options:���� $��Images\PasteText.png��$��24��$��24��$ M��$A Paste Options:���� $��Images\PasteTextOnly.png��$��24��$��24��$ T��$A Paste Options:���� $��Images\PasteFormating.png��$��24��$��24��$ L��$A Paste Options:���� $%��Images\PasteSourceFormating.png��$��24��$��24��$ E��$A Paste Options:����    Fluent.MenuItem  $��Set Default Paste...��$ A��B   	IsSplitedB . C m� Command$C Paste��.@�  -
   $��1��  $��2��$C Paste��  $��	Paste now��$C Paste��  -   D  Name$D 	cutButton��$ X��$	 Cut��$ Images\Cut.png��E   SizeDefinition$E Middle,Small��$	��Cut��    $! Cut (Ctrl+X)��$(" "Cut the contents to the Clipboard.��$%% Cut is currently not available.��$	��190��  -   $D 
copyButton��$ C��$
 Copy��$ Images\Copy.png��$E Middle,Small��$
��Copy��  -   $ FP��$ Format Painter��$ Images\FormatPainter.png��$E Middle,Small��
  -   $��buttonWatermark��$ 	Watermark��$ Images\watermark32.png��$ Images\watermark32.png��{�. $	��Top=�.@�  -   F  Name$F WatermarkGallery��$	9 Tag��G   MinItemsInRow$G 1��H   MaxItemsInRow$H 3��$8 
Horizontal=�$	; 100Q�$	< 100Q�I   SelectedFilterI �� $* galleryFilterAll3��J   FiltersJ    Fluent.GalleryGroupFilter  K   Title$	K All��L   Groups$%L Confidential,Disclaimers,Urgent��-   galleryFilterAll3��  $K Confidential��$L Confidential��  $K Disclaimers��$L Disclaimers��  $K Urgent��$L Urgent��.@��� $A Confidential��$��4q�
������ ._��� .+��� $	��Top=�$��Images\confidential.png��$
��None=��� $��CONFIDENTIAL 1��$��Bottom=�$��Center=��� $A Confidential��$��4q�
������ ._��� .+��� $	��Top=�$��Images\confidential2.png��$
��None=��� $��CONFIDENTIAL 2��$��Bottom=�$��Center=��� $A Confidential��$��4q�
������ ._��� .+��� $	��Top=�$��Images\donotcopy1.png��$
��None=��� $��DO NOT COPY 1��$��Bottom=�$��Center=��� $A Confidential��$��4q�
������ ._��� .+��� $	��Top=�$��Images\donotcopy2.png��$
��None=��� $��DO NOT COPY 2��$��Bottom=�$��Center=��� -   M ��Name$M Disclaimers��$A Disclaimers��$��4q�
������ ._��� .+��� $	��Top=�$��Images\draft1.png��$
��None=��� $��DRAFT 1��$��Bottom=�$��Center=��� $A Disclaimers��$��4q�
������ ._��� .+��� $	��Top=�$��Images\draft2.png��$
��None=��� $��DRAFT 2��$��Bottom=�$��Center=��� $A Disclaimers��$��4q�
������ ._��� .+��� $	��Top=�$��Images\sample1.png��$
��None=��� $��SAMPLE 1��$��Bottom=�$��Center=��� $A Disclaimers��$��4q�
������ ._��� .+��� $	��Top=�$��Images\sample2.png��$
��None=��� $��SAMPLE 2��$��Bottom=�$��Center=��� -   $M Urgent��$A Urgent��$��4q�
������ ._��� .+��� $	��Top=�$��Images\asap1.png��$
��None=��� $��ASAP 1��$��Bottom=�$��Center=��� $A Urgent��$��4q�
������ ._��� .+��� $	��Top=�$��Images\asap2.png��$
��None=��� $��ASAP 2��$��Bottom=�$��Center=��� $A Urgent��$��4q�
������ ._��� .+��� $	��Top=�$��Images\urgent1.png��$
��None=��� $��URGENT 1��$��Bottom=�$��Center=��� $A Urgent��$��4q�
������ ._��� .+��� $	��Top=�$��Images\urgent2.png��$
��None=��� $��URGENT 2��$��Bottom=�$��Center=�  -   $��buttonRemoveWatermark��$��Remove Watermark��N m� Icon$"N Images\removeWatermark16.png��O   Size$O Middle=�{�.   -   $
0 Font��$ ZF��$
2 Font��3 . $M1 Gpack://application:,,,/CMS.WPFHeadOffice;component/Images/FontColor.png��P   LauncherIcon$P /Images/FontColor.png��$4 NF��.@�   Fluent.RibbonToolBar  Q   LayoutDefinitionsQ *   $Fluent.RibbonToolBarLayoutDefinition  R   Size$R Large=�S   Rows.S    Fluent.RibbonToolBarRow  T   Children.T +   %Fluent.RibbonToolBarControlDefinition  U   Target$U comboBoxFontName��V   Width$V 75Q�  $U comboBoxFontSize��$V 45Q�0   *Fluent.RibbonToolBarControlGroupDefinition  W   Children.W   $U buttonGrowFont��  $U buttonShrinkFont��  .W   $U buttonChangeCase��  .W   $U buttonClearFormatting��  .T   .W   $U 
buttonBold��  $U buttonItalic��  $U buttonUnderline��  $U buttonStrikethrough��  $U buttonSubscript��  $U buttonSuperscript��  .W   $U buttonTextEffects��  $U buttonTextHighlightColor��  $U buttonFontColor��  $R Middle=�.S   .T   $U comboBoxFontName��$	V 125Q�  $U comboBoxFontSize��$V 50Q�  .T   .W   $U 
buttonBold��  $U buttonItalic��  $U buttonUnderline��  $U buttonStrikethrough��  $U buttonSubscript��  $U buttonSuperscript��  .W   $U buttonClearFormatting��  .T   .W   $U buttonTextEffects��  $U buttonTextHighlightColor��  $U buttonFontColor��  $U buttonChangeCase��  .W   $U buttonGrowFont��  $U buttonShrinkFont��X   Children.X 	  -   Y 	 Name$Y comboBoxFontName��$��40��$��22��
����������	#FF222222Z 	  GroupBy$	Z Tag��[ �� DisplayMemberPath$
[ Text��\ �� IsTextSearchEnabled\ . ] 	  
ResizeMode$] Vertical=�$ FF��^ 	  SizeDefinition$^ Small��$ 0��.@��� $��Arial��$A Theme Fonts���� $��Tahoma��$A Theme Fonts���� $��Tahoma��$A Recent Used Fonts���� $��Arial��$A AllFonts���� -   _ ��Name$_ 	segoeFont��$��Segoe UI��$A AllFonts���� $��Tahoma��$A AllFonts���� $��Webdings��$A AllFonts���� $��Winding��$A AllFonts��	  -   $Y comboBoxFontSize��$��49��$
��Left=�$��22��$ 1��
����������	#FF222222` �� Text$` 12��$
[ Text��a �� 
IsEditablea .  $^ Small��$] Vertical=�$ FS��.@��� $��7���� $��8���� $��9���� $��10���� $��11���� $��12���� $��14���� $��16���� $��18���� $��20���� $��22���� $��24���� $��28���� $��32���� $��36���� $��48���� $��72��  -   $D buttonGrowFont��$ GF��$ Images\GrowFont.png��$
��Left=�$E Small��  -   $D buttonShrinkFont��$ Images\ShrinkFont.png��$
��Left=�$E Small��
  -   b 
 Name$b buttonChangeCase��$ Images\ChangeCase.png��$
��Left=�$6 Small��  -   $D buttonClearFormatting��$  Images\ClearFormatting.png��$
��Left=�$E Small��   Fluent.ToggleButton  -   c  Name$c 
buttonBold��$ B��d   Icon$d Images\Bold.png��$
��Left=�e   SizeDefinition$e Small��
  -   $b buttonUnderline��$ Images\Underline.png��$
��Left=�$6 Small��  -   $D buttonStrikethrough��$ Images\Strikethrough.png��$
��Left=�$E Small��  -   $D buttonSubscript��$ Images\Subscript.png��$
��Left=�$E Small��  -    $D buttonSuperscript��$ Images\Superscript.png��$
��Left=�$E Small��
  -!   $b buttonTextEffects��$
��Left=�$6 Small�� � .+��� $��Images\TextEffects.png��$��16��$��16���� $��Bottom=�$��2��$��16��$��Center=����� f �� Colorf �� *SelectedColor+$* standardColorGallery��.@�   Fluent.ColorGallery  -"   g  Name$g standardColorGallery��h   Mode$h StandardColors=�i   SelectedColori �� *SelectedColor+$* highlightColorGallery��$ TwoWay=�
  -#   $b buttonTextHighlightColor��$
��Left=�$6 Small�� � .+��� $#��Images\TextHighlightColor.png��$��16��$��16���� $��Bottom=�$��4��$��16��$��Center=����� f �� *SelectedColor+$* highlightColorGallery��.@�  -$   $g highlightColorGallery��$h HighlightColors=�$i YellowX�$j   IsAutomaticColorButtonVisiblej .  
  -%   $b buttonFontColor��$
��Left=�$6 Small��k 
  MaxDropDownHeight$	k 500Q� � .+��� $��Images\FontColor.png��$��16��$��16��l J� SnapsToDevicePixelsl . �� $��Bottom=�$��4��$��16��$��Center=����� f �� *SelectedColor+$* themeColorGallery��.@�  -&   $g themeColorGallery��$
i BlueX�$h ThemeColors=�m   StandardColorGridRows$m 3��n   Columns$n 10��o   ThemeColorGridRows$o 5��p   IsNoColorButtonVisiblep . q   ThemeColorsSourceq �� *ThemeColors+  -'   $0 A��$K1 Epack://application:,,,/CMS.WPFHeadOffice;component/Images/Default.png��$ A��$2 	Galleries��3 . .@�   Fluent.InRibbonGallery  -(   r  Name$r inRibbonGallery1��s   Icon$s Images/Default.png��t   	LargeIcon$t Images/GalleryLarge.png��u   Header$u In-Ribbon Gallery��$ JS��v   
ResizeMode$
v Both=�w   MinItemsInRow$w 2��x   MaxItemsInRow$x 7��y   SizeDefinition$y Large��z   Orientation$z 
Horizontal=�{   	ItemWidth${ 40Q�|   
ItemHeight$| 56Q�}   GroupBy$	} Tag��~   CanCollapseToButton~ .    MinItemsInDropDownRow$ 4���   SelectedFilter� �� $* galleryFilterAll���   Filters�   $	K All��$L Group1,Group2��-)   galleryFilterAll��  $K Group1 L��$L Group1��  $K Group2 L��$L Group2��.@��� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�1�� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�2�� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�3�� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�4�� -*   $	M ghh��$A Group1��$��1q�
����� �._��� $��Center=�$��Center=�33�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�5�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�6�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�7  -+   $ Images/Default.png��$ /Images/GalleryLarge.png��$ Split��$ S��� 
  
ResizeMode$
� Both=�$6 Large��$
k 1000Q�� 
  DropDownHeight$	� 300Q�.@�  -,   $F gallery��$��60��$G 2��$H 6��$	; 128Q�$	< 128Q�.@��� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�1�� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�2�� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�3�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�4�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�5�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�6�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�7�� $A Group3��$��1q�
����� �._��� $��Center=�$��Center=�8  $��Item5��$ R��.@�  $��Itemq��$ A��  $��Itemw��$ B��  $��Item5��$ R��$N Images\ChangeCase.png��.@�  $��Itemq��$ A���   MaxDropDownHeight$	� 350Q�.@�  --   $F galleryl��$��60��$G 3��$H 6��$; 28Q�$< 28Q�.@��� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�1�� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�2�� $A Group1��$��1q�
����� �._��� $��Center=�$��Center=�3�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�4�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�5�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�6�� $A Group2��$��1q�
����� �._��� $��Center=�$��Center=�7�� $A Group3��$��1q�
����� �._��� $��Center=�$��Center=�8  $��Itemw��$ B��  $��Item5��$ R��.@�  $��Itemq��$ A��� m� IsCheckable� . �   	GroupName$	� ABC��  $��Itemw��$ B��� . $	� ABC��  $��Iteme��$ C��� . $	� ABC��( .   $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  $
��Item��  -.   $0 C��$K1 Epack://application:,,,/CMS.WPFHeadOffice;component/Images/Default.png��$	 ZXK��$2 Themes��3 .  .@�  -/   $ Images/SilverTheme.png��$E Middle��. Silver  -0   $ Images/BlackTheme.png��$E Middle��. Black  -1   $ Images/BlueTheme.png��$E Middle��. Blue  -2   $0 B��$K1 Epack://application:,,,/CMS.WPFHeadOffice;component/Images/Default.png��$	 ZXB��$2 Spinners��3 . .@�  $ KA��$ 75Q�$
 0 px��$ Left:���   Icon$W� Qpack://application:,,,/Fluent;component/Themes/Office2010/Images/WarningSmall.png��{�.    $ KB��$ 75Q�$
 0 px��$ Right:��$W� Qpack://application:,,,/Fluent;component/Themes/Office2010/Images/WarningSmall.png��  $ KC��$ 75Q�$
 0 px��$
 Top:��$W� Qpack://application:,,,/Fluent;component/Themes/Office2010/Images/WarningSmall.png��  -3   $, 	secondTab��$ Y��$- Invoices���   IsSeparatorVisible� . $R. LgroupLL, groupLL, groupLL, (groupLL),(groupLL),(groupLL),(groupLL),(groupLL)��./   -4   $0 groupLL��$ FG��$2  ��3 .  .@�	  a .  � 	  
InputWidth$	� 150Q�� 	  Header$
� Test������ *	FontsData+���� .R��� ���� ���� 	  a .  $	� 150Q�$
� Test������ *	FontsData+���� .R��� ���� ���� 	  -5   $��FilterComboBox��� �� 
IsReadOnly� . $ H��$ 0��.@���Show All����	Read-only����	Writeable��  -6   $r inRibbonGallery��$Ws Qpack://application:,,,/Fluent;component/Themes/Office2010/Images/WarningSmall.png��$u Split��$ S��$	} Tag��$
v Both=�$x 6��$w 2��${ 40Q�$| 55Q�$ 5���   $	K All��$L Group1,Group2��-7   galleryFilterAll2��  $K Group1��$L Group1��  $K Group2��$L Group2��.@��� $A Group1��1�� $A Group1��2�� $A Group1��3�� $A Group1��4�� $A Group1��5�� $A Group1��6�� $A Group2��	1 (G 2)�� $A Group2��	2 (G 2)�� $A Group2��	3 (G 2)  -8   $ Images\GrowFont.png��$ Images\GrowFont.png��$ Enlarge��  -9   $ Images\ShrinkFont.png��$ Images\ShrinkFont.png��$ Reduce��  $2 LongLongGroup��3 .  .@�  $E Large,Large,Large��  $- 
WhareHouse��$	 Ü1��./   $2 LongLongGroup��3 .  .@�   Fluent.CheckBox  �   Header.� Text 1  .� Text 2  .� Text 3  $- Statics��$	 Ü2��./   -:   $	0 xxx��$2 LongLongGroup��3 . �   LauncherCommand$� Paste��.@�  $ Button��$ Images\GrowFont.png��$��ApplicationCommands.Paste��  $ Button��$ Images\GrowFont.png��$��ApplicationCommands.Paste����   $ Button��$ Images\GrowFont.png��$��ApplicationCommands.Paste��  $ Button��$ Images\GrowFont.png��$��ApplicationCommands.Paste����   $ Button��$ Images\GrowFont.png��$��ApplicationCommands.Paste��  .@�  �   Header$� Pause��$d Images\GrowFont.png��$e Middle���   	GroupName$� Group1��  $#� Button long text ggggggg gggg���   	LargeIcon$� Images\Test32.png��$d Images\Test16.png��$e Middle��$� Group1����   $#� Button long text ggggggg gggg��$� Images\Test32.png��$d Images\Test16.png��$� Group1��  $� Button1��$� Group1����   $� Button1��$� Group1��  .@�  $� Button1��$� Group1��  $� Button2��$� Group2��  $- Configuration��./   .@�	  -;   $Y 	someCombo��$� 80Q�a .  ���� *TstArr+$* window��� �� SelectedItem� �� *TST+$ TwoWay=�$* window��  $� Button1��$� Group1��  $� Button1��$� Group1��  $� Button1��$� Group1��"  ControlLibrary.ModernContent  $��1���   ContentSource$� /Pages/Introduction.xaml?��   LogoData�� ��|�{h���l�~���ӴL����$:�<��C<��p�<L^��������ф�̴�ӄ��n�$�W`�F�W���d�W<�w@Xt�}̴�� ~o�"%���:���C�:��P���:� -�"%��	h�����@Xt0�"��W��@��W�|�c��W|�{�n|�{h���܄��:�����:`-�m``-����`-��
����(d�܄�(d��P6i(d���B�
���B��Ӽ�Bm`P6i�:܄��:�$&��:Ӱt�:D�Dm`D�D���D�D�
��t(d�$&�(d�Ә׿(d��}��
��}���Ӡ}�m`�׿�:$&��:�   MenuLinkGroups�    ControlLibrary.LinkGroup    !  ControlLibrary.Displayable� !  DisplayName$� Welcome���    Links� "  ControlLibrary.Link"  $� Introduction��� "  Source$� /Pages/Introduction.xaml?�   $� Layout��� "  $� 	Wireframe��$!� /Pages/LayoutWireframe.xaml?�"  $� Basic��$� /Pages/LayoutBasic.xaml?�"  $� Split��$� /Pages/LayoutSplit.xaml?�"  $
� List��$� /Pages/LayoutList.xaml?�"  $	� Tab��$� /Pages/LayoutTab.xaml?�   $� Controls��� "  $� Styles��$ � /Pages/ControlsStyles.xaml?�"  $� Modern controls��$ � /Pages/ControlsModern.xaml?�   $� Advanced��� "  $� 
Navigation��$� /Pages/Navigation.xaml?�"  $� Content Loader��$� /Pages/ContentLoader.xaml?�   $� Settings���    	GroupName$� settings��� "  $� Software��$� /Pages/Settings.xaml?�#   Fluent.StatusBar#  $��Stretch=�$��Bottom=�$��2��.@�$   Fluent.StatusBarItem$  � $  Title$� Words Width��� $  Value$	� 150��$
��Left=�.���� $��150 px��
��������� $
��Left=�$  $� Selected Words��$� 15��$  This is Selected Words��$��15��$
��Left=�
��������� $
��Left=�$  $
� Zoom��$��Right=�� �� *Value+$* 
zoomSlider��� �� StringFormat$� {0:0}%��$  $� Zoom Slider��$��Right=�.���� -<   � ��Name$� 
zoomSlider��� � Minimum$� 20Q�� � Maximum$	� 500Q�� �� IsDirectionReversed� .    ZoomSliderStyle#���  � � Value$	� 100Q�
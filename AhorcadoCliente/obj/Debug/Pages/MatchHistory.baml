   M S B A M L   `   `   `  ����J  FAhorcadoCliente, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AhorcadoCliente.PagesAhorcadoCliente.Pages  5      G(clr-namespace:AhorcadoCliente.PropertiesAhorcadoCliente.Properties  5      (    "AhorcadoCliente.Pages.MatchHistory   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AhorcadoCliente.Pages     5      6p(clr-namespace:AhorcadoCliente.Properties     5        N� Title$  MatchHistory��5
      .��5      � ���#4d9eed � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� *   $AhorcadoCliente.Properties.Resources   MatchHistoryLabel#��Z 6   $��	Monocraft)�6C   $��Center=�6Z   $��0,10,0,0q�6w   $	��Top=�6�   $��50(�6�   $
��Bold"�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ HistoryDataGrid��5         AutoGenerateColumns .     CanUserResizeRows .     CanUserReorderColumns .     CanUserAddRows .     CanUserSortColumns .  	   
IsReadOnly	 . $	��Top=�5   %   $��1��6=   $��
50,72,50,0q�6J   
   CanUserResizeColumns
 .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      %   DataGridColumnChallengerDefeat,  &System.Windows.Controls.DataGridColumn   Header# Z 6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6d   �� *WinnerNickname+6�     5         DataGridColumnDate# Z 6%   $ * 6v    6X   �� *	DateMatch+6v   5      � 5   
   $��2��6   $��Stretch=�6   $��10,10,10,10q�6:   $��Bottom=�6O    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5       $��* �6#   .+�5"      �� -   5        $��Volver��5"      $	��130��6&   $��30��62   $��325,0,325,0q�6>   $��0��6S   $��	Monocraft)�5#   	   ���#D6D5A8$��14(�65   $��Medium"�6C    � Cursor$
 Hand��6W   ���#28c4fc$��2��5$      ��5%      �� 5&       �� 
TargetType" ��6    �� Triggers 5'      P� 5(       P� Property �@J�IsMouseOver P� Value . . �5)   "   ��  �� Property � �� �� Value �#1B24306?   5*      5,      ��5.      �� 5/       �� 
TargetType" ��6&   .R�50      �� $	��130��6!   $��30��6-    �� CornerRadius$ 6��69   #��z��6J   ._�51      �� $��Center=�6/   $��Center=�6J   52      53      55      56      �� 57   
   -   5         ��Name$ labelPointsEarned��57      $�� ��6+   $
��Left=�66   $��	50,28,0,0q�6Q   $��1��6d   $	��Top=�6q   $��20(�6�   $
��Bold"�6�   58      59      
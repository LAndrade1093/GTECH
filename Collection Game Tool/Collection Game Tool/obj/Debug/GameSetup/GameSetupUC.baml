   M S B A M L   `   `   `  ����O  KCollection Game Tool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Collection_Game_Tool.ServicesCollection_Game_Tool.Services  5      O,clr-namespace:Collection_Game_Tool.GameSetupCollection_Game_Tool.GameSetup  5      0    *Collection_Game_Tool.GameSetup.GameSetupUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @Services+clr-namespace:Collection_Game_Tool.Services     5      >local,clr-namespace:Collection_Game_Tool.GameSetup     5      -   5          >�Name$  GameSetupUserControl��5      .��5      �� 
���   �$��2,0,2,0q�6!   ._�5   
   � -   5         �Name$ GameSetupMainPanel��5      ���#575757$	��490��5       � RowDefinitions 5      �� 5      $��50 �6    �� 5      $��60 �6    �� 5      $��* �6     � 	Resources 5      :   4Collection_Game_Tool.Services.CreateOpacityConverter   OpacityConverter  instructionStyle �� 
TargetType �� Property �� Value  
titleStyle  dataLabelStyle  externalBorderStyle��%  &	        &	      &	 �     &	 �    &	 /      5      62   �� 5      " ��61   .�5      ��  � �� �#009ad863   �� 5       � ��
 �����63   �� 5       � ��$ Stretch=�6<   �� 5       � ��$ Center=�6C   5      �� 5      " ��6+   .�5      ��  � ��
 �����63   �� 5        � �� �#007CAA63   �� 5!       � ��$ Stretch=�6<   �� 5"       � ��$ Center=�6C   �� 5#       � ��$ 18(�61   �� 5$       � ��$
 Bold"�63   5%      �� 5&      " ��6/   .�5'      ��  � ��$ 14(�61   5(      �� 5)      " ��64   .�5*      ��  � ��
 �   �64   �� 5+       � ��$ 2q�68   �� 5,       � ��$ 0,0,0,10q�6/   �� 5-       � ��$	 430��61   5.      50      .+�51      �� $��0��6   $��Stretch=�6!   $��Center=�6?   ���#0068ad
�������$��30(�6�   $
��Bold"�6�   .��6�   
Game Setup6�   �� 52      $��1��6   #��[ 6!   $��0,0,0,10q�6K   $��50��6]   $��The basic game settings��6i   $��Center=�6�   6�   �� 54      -   5         ��Name$ gameSetupScroll��54   (   $��2��6   $	��800��6A   $	��Top=�6Q   $
��Auto=�6i   .��56      �� -   5         ��Name$ GameSetupOptionsPanel��56      $
��10,0q�57      .+�58      �� #��[ 6   ._�59      �� -   5        $ GSPlayerPicksPanel��59   %   
�������.+�5;      �� #��[ 6$   .��6H   Player Picks6V   �� 5<      
���   �$��0,0,0,2q�69   ._�5=   "   �� #��[ 6(   .��6R   ?=The number of picks the player gets to attempt to win a prize6�   5>      �� 5?      	 �� Orientation$	 
Horizontal=�6)   ���#ADADAD.+�5@   "   �� #��[ 6(   .��6P   Total Number:6_   �� 5A   "   -   5        
 ��Name$
 NumPicksLabel��5A   (    �� ContentStringFormat$ {0:#}��5C   (   $��14(�5D   (   $
��Bold"�5E   (   $��Center=�5F   (   $��Center=�5G   (   
�������$��30��5I   (   $��0,0,30,0q�5J   (   ��5B   (   ��  �� ElementName$ TotalPicksSlider�� �� Path$ Value�5J   <   �� 5K   "   $	 
Horizontal=�6-   $
��10,0q�6F   .+�5L   &   �� $��14(�6,   .��6:   16=   �� 5M   &   -   5         ��Name$ TotalPicksSlider��5M   -    � Value$ 2Q�5N   -   $	��200��5P   -    � Maximum$ 20Q�6<    � Minimum$ 1Q�6I    �� IsSelectionRangeEnabled . $��Center=�5R   -    �� IsSnapToTickEnabled .  �� TickPlacement$
 Both=�5S   H    �� TickFrequency$ 1Q�6]   6q   �� 5T   &   $��14(�6,   .��6:   206>   5U   #   5W      5Y      5Z      �� 5\      #��[ 6   ._�5]      �� -   5        $ GSNearWinPanel��5]   %   ���#ADADAD.+�5^      �� #��[ 6$   .��6H   Near Win Experience6]   �� 5_      
���   �$��0,0,0,2q�69   ._�5`   "   �� #��[ 6(   .��6R   SQIf checked, defines how many levels come within one collection of winning a prize6�   5a      �� 5b      
���   �$��0,0,0,2q�69   ._�5c   "   �� $	 
Horizontal=�6-   .+�5d   &   �� #��[ 6,   .��6T   Enforce Near Win Experience:6r   �� 5e   &   -	   5         ��Name$ NearWinCheckbox��5e   /   $��Center=�6f   6�   5f   #   5g      �� 5h      -
   5        $ GSNearWinOptional��5h   �   $	 
Horizontal=�6k   ���#ADADAD{�6)   �� $ NearWinCheckbox��$ 	IsChecked�.+�5i   "   �� #��[ 6(   .��6P   Total Near Wins:6b   �� 5j   "   -   5        $
 NumNearWinsLabel��5j   (   $��14(�5l   (   $
��Bold"�5m   (   $ {0:#}��5n   (   $��Center=�5o   (   
�������$��30��5q   (   $��0,0,30,0q�5r   (   ��5k   (   �� $ NumNearWinsSlider��$ Value�5r   <   �� 5s   "   $	 
Horizontal=�6-   $
��10,0q�6F   .+�5t   &   �� $��14(�6,   .��6:   16=   �� 5u   &   -   5        $ NumNearWinsSlider��5u   -   $	��200��5w   -   $ 12Q�6<   $ 1Q�6I    . $��Center=�5y   -    . $
 Both=�5z   H   $ 1Q�6]   6q   �� 5{   &   $��14(�6,   .��6:   126>   5|   #   5}      5      5�      �� 5�      #��[ 6   ._�5�      �� -   5        $ GSPermutationsPanel��5�   %   ���#ADADAD.+�5�      �� #��[ 6$   .��6H   Game Win/Loss Permutations6d   �� 5�      
���   �$��0,0,0,2q�69   ._�5�   "   �� .+�5�   &   �� #��[ 6,   $	��0,2q�6V   .��6d   DBThe number of winning combinations to calculate for each division,6�   �� 5�   &   #��[ 6,   $��0,0,0,2q�6V   .��6h   $"including an extra losing division6�   5�   #   5�      ]� 5�      .+�5�   "   �� #��[ 6(   .��6P   Maximum Permutations:6g   �� 5�   "   -   5         ��Name$ MaxPermutationsTextBox��5�   *   $��14(�5�   &   $��Center=�5�   &   $
��Left=�5�   &   $	��100��5�   &   .��5�   +   16.   5�      5�      5�      �� 5�      -   5         ��Name$ CreateButton��5�      $	��200��6N   $��50��6Z   $��28(�6f   $
��Bold"�6t   ���#007CAA
�������$��0,30,0,0q�6�   {�6�   �� $ 	canCreate� J� Opacity 6�   �� $ 	canCreate� �� 	Converter# [  .��65  Create File6B  �� 5�      -   5        $ GeneratingFileAnimation��5�   !   $��10q�6B   $y�Hidden=�6N   .+�5�      �� $��20(�6    $��Center=�6.   .��6K   Generating File6\   (� 5�      $��50��6"   $��50��6-   $��Center=�5�      $��Center=�5�      .��5�      8   2Collection_Game_Tool.GameSetup.CircularProgressBar  5�      5�      �� 5�      -   5        $
 GeneratingCompleteMessage��5�      $��20(�6?   $��10q�6M   $��Center=�6Y   $y�Hidden=�6v   .��6�   File Generated!6�   �� 5�      -   5         ��Name$ ErrorBoxBorder��5�      $��4q�65   
���   �$��0,0,0,10q�6]   ._�5�      �� -   5        $ errorPanelScroll��5�   '   $	��350��6A   .��5�      �� 
�������.+�5�   "   �� $
��Bold"�6(   $��14(�6:   ���#D60804
�������.��6p   	Errors:6y   �� 5�   "   -   5         ��Name$ ErrorTextBlock��5�   ,   $	��400��6D   
���   �$
��Wrap=�6f   ��6�   �� $ 	errorText� �� Mode$ TwoWay=� �� NotifyOnTargetUpdated . 6�   �� 5�   "   $
��Bold"�6(   $��14(�6:   ���#FFEE00
���   �.��6p   	Warnings:6{   �� 5�   "   -   5        $ WarningTextBlock��5�   ,   $	��400��6F   
���   �$
��Wrap=�6h   ��6�   �� $ warningText�$ TwoWay=� . 6�   5�      5�      5�      5�      5�      5�      5�      5�      
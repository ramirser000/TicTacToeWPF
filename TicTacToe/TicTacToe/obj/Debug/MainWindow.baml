   M S B A M L   `   `   `  ����D  @TicTacToe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:TicTacToe	TicTacToe  5   	   ;"clr-namespace:TicTacToe.MyCommandsTicTacToe.MyCommands  5   	       TicTacToe.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:TicTacToe     5   	   7commands"clr-namespace:TicTacToe.MyCommands     5   	   -   5          � Title$  
MainWindow��5	   	   $	��550��6   $	��525��6)    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ NoResize=�6Z   .��5
      �� .+�5   
   � ��6   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �� �� Path$ ActualWidth���5   6   ��  �� *FindAncestor+" ��$ ActualHeight�.+�5      � -   5        $��	gridBoard��5      $��Stretch=�5      $��Stretch=�5      $
��auto��5      $
��auto��5      $��50,76,50,100q�5      
���   �5      	� 5      -   5         	�Name$ radioButtonOnePlayer��5      $��
One Player��68   $
��Left=�6M   $��
50,441,0,0q�6h   $	��Top=�6|    d� 	IsChecked$
 TrueY�6�   	� 5      -   5        $ radioButtonTwoPlayers��5      $��Two Players��69   $
��Left=�6O   $��
50,461,0,0q�6j   $	��Top=�6~   �� 5      
���   �$��1q�6)   $
��Left=�6=   $��59��6X   $��
26,430,0,0q�6d   $	��Top=�6x   $	��119��6�   �� 5      -   5         ��Name$ buttonStart��5      $��
Start Game��6*   #   TicTacToe.MyCommands.Commands	   startCommand#��Z	 6?   $
��Left=�6s   $��376,446,0,0q�6�   $	��Top=�6�   $��91��6�   $��30��6�   �� 5      -   5        
 ��Name$
 	imageCoin��5      $
��Left=�6'   $��78��6B   $��224,421,0,0q�6N   $	��Top=�6c   $��85��6{   �� 5      -   5         ��Name$ labelAnnounce��5      $�� ��6+   $
��Left=�66   $��
180,31,0,0q�6Q   $	��Top=�6e   $	��163��6}   $��14(�6�   $
��Bold"�6�   ���	#00000000���	#FF181FBB6�   �� 5      -   5        $ labelO��5      $�� ��6$   $
��Left=�6/   $��	50,31,0,0q�6J   $	��Top=�6]   $	��125��6u   $��14(�6�   $
��Bold"�6�   ���	#FFEC680D6�   �� 5      -	   5        $ labelX��5      $�� ��6$   $
��Left=�6/   $��
349,31,0,0q�6J   $	��Top=�6^   $	��120��6v   ���	#FF187C08$��14(�6�   $
��Bold"�6�   5      o� 5    
   -
   5         o�Name$ menuBar��5       .@�5!      m� -   5         m�Name$ menuItemGame��5!      $��_Game��6-   .@�5"      m� -   5        $ menuItemStart��5"      $��	_Start...��62    m� Command# Z	 6E    m� InputGestureText$ Ctrl+S��6y   m� 5#      -   5        $ menuItemResetSession��5#      $��_Reset Session��69      restartCommand# Z 6Q   $ Ctrl+R��6�   m� 5$      -   5        $ menuItemExit��5$      $��_Exit��61      exitCommand# Z 6@   $ Ctrl+E��6s   5%      m� 5&      -   5        $ menuItemHelp��5&      $��_Help��6-   .@�5'      m� -   5        $ menuItemAbout��5'      $��_About��62      helpCommand# Z 6B   $ Ctrl+H��6u   m� 5(      -   5        $ menuItemRules��5(      $��_Rules��62      rulesCommand# Z 6B   $ Ctrl+L��6v   m� 5)      -   5        $ menuItemShotCuts��5)      $��_Keyboard Shortcuts��65      shortCutsCommand# Z 6R   $ Ctrl+K��6�   5*      5,      5-       J� CommandBindings 50      )  #System.Windows.Input.CommandBinding  51   
   -   5           Command# Z	 51      6M     52   
   -   5        # Z 52      6O     53   
   -   5        # Z 53      6L     54   
   -   5        # Z 54      6L     55   
   -   5        # Z 55      6M     56   
   -   5        # Z 56      6Q    J� InputBindings 58      �� 59   
   
 �� Key$ S��6    �� 	Modifiers$ Controlf�6    �� Command# Z	 61   �� 5:   
   $ R��6   $ Controlf�6   # Z 61   �� 5;   
   $ E��6   $ Controlf�6   # Z 61   �� 5<   
   $ H��6   $ Controlf�6   # Z 61   �� 5=   
   $ L��6   $ Controlf�6   # Z 61   �� 5>   
   $ K��6   $ Controlf�6   # Z 61   5A      
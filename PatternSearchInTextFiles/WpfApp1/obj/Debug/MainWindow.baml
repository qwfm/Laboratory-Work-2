   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  
MainWindow��5   	   $	��545��6   $
��1161��6)   .��5	      � 
�������$��Bottom=�6"   $	��545��6=   $��	0,0,0,-16q�6J    � RowDefinitions 5
   
   �� 5      $	��18* �6   �� 5      $	��91* �6   .+�5   
   �� $��Text pattern searcher��6   $��268,17,268,19q�60   $��Center=�6G   $��30(�6k   $
��Bold"�6y   �� 5   
   -   5        $��Insert file��5      $��23,14,1022,405q�6'   $��18(�6\   $��DemiBold"�6j   $��1��6�   �� 5   
   -   5        $��Search Pattern��5      $��293,14,732,405q�6L   $��18(�6d   $��DemiBold"�6r   $��1��6�   �� 5   
   -   5        $
��Open��5      $
��Left=�6=   $	��Top=�6X   $��188,380,0,0q�6p   $��36��6�   $	��132��6�   $��1��6�   �� 5   
   -   5         ��Name$ richTextBoxOutput��5       �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6F   $ �� HorizontalScrollBarVisibility$
 Auto=�6i   $��
479,76,0,0q�6�   $��20(�6�   $
��Left=�6�   $	��657��6�   $	��417��6�   $	��Top=�6�   $��2��6�   �� 5   
   $��Your pattern:��6   $��92,50,841,349q�6(   $��20(�6?   $��Center=�6M   $	��228��6q   $��1��6}   �� 5   
   -   5         ��Name$ PatternTextBox��5      $��23,89,732,349q�6+   $��1��6B   �� 5   
   -   5         ��Name$ listBoxSuccessfulPatterns��5      $��23,167,732,140q�6�   $��DemiBold"�6�   $��15(�6�   $��1��6�   �� 5   
   $��Saved patterns:��6   $��92,111,841,288q�6*   $��20(�6B   $��Center=�6P   $	��228��6t   $��1��6�   �� 5   
   -   5        $��Delete Pattern��5      $
��Left=�6L   $	��Top=�6g   $��313,139,0,0q�6   $��23��6�   $	��116��6�   $��DemiBold"�6�   $��15(�6�   $��1��6�   �� 5   
   -   5         ��Name$ comboBoxFiles��5      $
��Left=�6*   $	��Top=�6E   $	��406��6]   $��
26,338,0,0q�6i   $��25��6}   $��1��6�   �� 5   
   	 �� Orientation$	 
Horizontal=�6   $��10q�6.   �� 5   
   -	   5        $��Pick Fragment��5      $
��Left=�6J   $��18(�6e   $��DemiBold"�6s   $	��Top=�6�   $��
140,14,0,0q�6�   $��36��6�   $	��132��6�   $��1��6�   z�5      V� 5      .~�5      �� �� 5      �� 5      T� 5       5!      5#      5$      5%      
   M S B A M L   `   `   `  ����F  BFoodRecipes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:FoodRecipesFoodRecipes  5   	       FoodRecipes.SplashScreen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:FoodRecipes     5   	   -   5          �Name$  splashScreen��5   	   $	��450��5	   	   $	��700��5
   	    � Icon$' !Assets/Images/FoodRecipesLogo.png��5   	   
������  � AllowsTransparency .  � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � Topmost .  � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=�5   	   .��5      � -   5         �Name$ splashScreenGrid��5         carrotColorBrush#��[  5      .+�5   
   �� -   5        	 ��Name$	 foodRecipiesLogo��5      $'��!Assets/Images/FoodRecipesLogo.png��5      $
��Fill=�5      $��Center=�5      $��Center=�5      $	��130��5      $	��160��5      $��	0 0 0 155q�5        blackShadowEffect
 J� Effect#
 [ 5       �� 5"   
   -   5         ��Name$ appName��5"      $��Food Recpies��5#      $��Center=�5$      $��Center=�5%      ���#95DE10$*��$/FoodRecipes;component/Fonts/#Tahoma)�5'      $��70(�5(      $
��Bold"�5)      $��0 60 0 0q�5*      #
 [ 5+      �� 5-   
   -   5         ��Name$ splashScreenDisabledPanel��5-       �� Orientation$ 
Horizontal=�5.      $
��Left=�5/      $��Bottom=�50      $��5q�51      .+�52      �� -   5         ��Name$" splashScreenDisabledCheckBox��52      ��.  $��0 0 5 0q�54        tickCheckboxStyle#��[ 55      57      �� 58      -   5        $ splashScreenDisabledText��58      $?��9Không hiện hộp thoại này mỗi khi khởi động��59        normalTextblockStyle#��[ 5:      5;      �� 5=   
   -   5        $	 tip��5=      $��Mẹo: ��5>      $
��Left=�5?      $	��Top=�5@      $��0 5 0 5q�5A      #��[ 5B       � Triggers 5C      :� 5D       :� RoutedEvent$ Loaded��6   .[�5E      �� .��5F      �� .��5G      t�  �� 
TargetName$	 tip��61    �� TargetProperty$ Margin�5H   1    t� From$ 	768,5,0,5X�5I   1   	 t� To$ -2268,5,0,5X�5J   1    g� 	BeginTime$ 0:0:0X�5K   1    g� Duration$ 0:0:22D�5L   1   5M      5N      5O      5Q      �� 5S   
   -	   5        $ welComeText��5S      $!��Welcome to Food Recpies App��5T      $��Center=�5U      $��Center=�5V      $*��$/FoodRecipes;component/Fonts/#Tahoma)�5W      
�������$��20(�5Y      $��	0 185 0 0q�5Z      #
 [ 5[      �� 5]   
   -
   5         ��Name$ 
skipButton��5]      $��Right=�5^      $��Bottom=�5_      $��0q�5`      
������ ��.  $��5q�5c        skipButtonStyle#��[ 5d      5e       5f   
   :� 5g      $ Loaded��6   .[�5h      �� .��5i      �� .��5j      W� $ splashScreenGrid��6*   $ Opacity�5k   *    W� From$ 0X�5l   *   	 W� To$ 1X�5m   *   $ 0:0:0.5D�5n   *   5o      5p      5q      5s      5t      
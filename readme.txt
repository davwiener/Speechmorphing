My main goal in this project was to find a way to get easy access for each json token.
I was able to do so by building a class that will cover a json object. In this class we will have a 
Dictionary of all the keys and values of the JSON file and a List for id's (check offsets of id's as requested).

Packages/libraries and FrameWork:
I chose to do an WPF c#  app. The app built in a MVVM framework .
I use GalaSoft.MvvmLight nugget for this framework  and Newtonsoft.Json for paring JSON.

Flow:
The user will select tow object files . After a file as been chosen a new class of JsonCompare will be crated.
In the constructor A Dictionary and ID’s list will be created recursively by the helping function BuildJsonDic.
Later on when the user selected tow JSON file he can compare them. If he hasn’t chosen tow files an error
message will be created in the bottom of the screen.
On the pressed of Compare Json bottom will need to check if the first json Named A and the second json Named B 
are equal. Hence we will need to check if A = B and B = A. So will check if all the keys from A presented in B
and all the keys Of  B presented in A. Plus we will check if a values of A are in B (there no point doing both).
If the tow files are not equal we all the differences will be presented in the middle of the screen. 

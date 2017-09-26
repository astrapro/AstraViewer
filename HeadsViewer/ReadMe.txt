Some words about this sample application created by VectorDraw Team.
This sample is provided to our customers and can be used as a start-up application for further development.You can
add functions and menu/toolbar buttons easily to implement your application's needs. Also you can find usefull implementations
(like purge form or the raster export form) to help you with your application. We are not going to support this sample but you are
free to ask if you have any question and if possible we will help you. It is recomended to change the icons of this application
in order to fit your style,if you want to sell an application based on this sample,since these are distributed to all our clients.

---How to Add a New command to this application---
In order to add a new command/button to vdfCAD you should follow the next instructions(Lets add the Slice command).
1) Add the command to Menu.txt.
You should open the Menu.txt file and add a line like below to the menu you prefer the button to appear.
We will add the Slice command to the MainMenu = Modify like below
SubButton = Slice , Slice ,slice.ico ,True 
The above line folows this rule : Subbutton = Name(Text) , Command(added in the commands.txt) , Icon image,Boolean ExportToToolbar(can be empty means True)

For the Implementation of the above function you have 2 options:
-------------------------------------------------------------------------------------------------------------------------------------------------------
A) Create and implement a method to the ExtraMethods dll or any other dll:
1) Add the command to the commands.txt
You should open the commands.txt file and add a line like below
Slice,,ExtraMethods.dll,ExtraMethods.Methods,Slice
The above line means : Main command,optional command line call(can be empty),dll name,namespace,static function name
This means that we have to implement a Slice command at the ExtraMethods.Methods class with the following syntax
public static void Slice(vdDocument doc)

VectorDraw commandline will direct the code to this method passing the Document where the command will take place.

2) Now we have to implement the code for this function.The implementation goes to the Slice Method that we added to the ExtraMethods dll.
-------------------------------------------------------------------------------------------------------------------------------------------------------
B) Directly implement the method inside the application's code (if you need more than the passed Document parameter that is passed in the above implementation).
1) You can implement directly the code for this command to MainForm.cs at the CommandExecute Method with a syntax like below

else if (string.Compare(commandname, "Slice", true) == 0)
{
    //Implementation of the command
    success = true;
}

In the MainForm you have access to all controls of the application like property grid commandline and also active child window in order to fully implement your command.
-------------------------------------------------------------------------------------------------------------------------------------------------------

---How to Remove a command from the application---
You can simply remove a command from the application following the below steps:
1)Remove the line that adds the command to the menu and the toolbars from the Menu.txt file.
2)Remove the line that adds the command to the commands from the commands.txt
3)Finally delete the implementation code (if exist, some functions like Break are loaded from VectorDraw.Professional.dll and there is no implementation code in this sample.)
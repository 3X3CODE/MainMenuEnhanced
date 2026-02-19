# MainMenuEnhanced user-guide and official documentation <br>


## Features and Documentation<br>
### Custom menu background<br>
This feature changes the background image of the main menu to whichever image you place in your **BepInEx/plugins/** folder. Please note that the image should be either png, jpg or jpeg.<br>
The settings on how to change the workings of this feature will be discussed on the third feature.<br>
### Interactive Menu<br>
One of my most favorite features. This allows you to grab and move the Player Particles on the main screen. There is an issue with this feature regarding Object Pooling however I have temporarily fixed it. <br>
### Customizable Menu<br>
Both files, Json and XML are created by the mod if they don’t exist already along with a custom folder for those files.<br>


When first running the mod, the mod will create the new Folder and the two Json and XML files. During this process, the code running is disabled due to the values having template values that won’t work with the game.<br>


Once the files are created, you can edit them and restart the game.<br>


#### JSON<br>
One of the bigger features. This allows you to take hold of any GameObject in the game and do several functions with it. As followed below,<br>
* Run methods on the GameObject
* Find a component attached to the GameObject
* Find properties on the component
* Set values to the properties
* Change GameObject position<br>


All of these functions can be accessed through the Json file.<br>

Other than those features, in case you want to reset those values, you just need to set **”Save”: false**. Restart the game, and your Json file should be rewritten with the default settings.<br>


> [!NOTE]
> Please know that hot reloading is currently a work-in-progress for this tool.<br>

#### XML<br>
This file currently runs better and Hot-Reloading is supported. This file has less features than the Json, however I am planning to switch the settings entirely to this.<br>
This file works on an array, therefore you can edit as many objects as you’d like.<br>


As you may have noticed, each file has an option to set the EditActive and Position Active features. Setting “EditActive” to false, turns the code off entirely. The reason to check if the user wants the position editing to be on is because the default position values, **0**, changes the selected GameObjects position to 0 which changes the default position.<br>


> [!Warning]
> These files currently have an issue where if you set the GameObject to “inactive”, the code will not find it again. So editing properties after it will not run unless you restart the game.<br>


## Future Development Plans<br>
* Change particle sprites
* Add settings menu
* Button to add more particles
* Run Customizable Menu entirely on XML
* Add more features to the Customizable Menu
* Enhance performance
* I will start working on an Android build that will most likely be done before the end of February.<br>


- - -
Congrats! You’ve reached the end of this document. Which means you either like reading, interested in this mod or just have time lying around. Nonetheless, thank you for trying out this mod.<br>


# Thank You <br>
<sub>
3X3C - 2026
</sub>

My intention is to create an automated tool for Windows which can quickly and automatically produce the steps in the guide. These are the notes for that.



* ✓ Step 1: Exporting the uasset to psk and textures to tga
  * Possible, umodel is a command line tool with all options available in the CLI
* ✓ Step 2: Importing the pskx Mesh in Blender
  * We can run scripts in headless mode in Blender: <https://caretdashcaret.com/2015/05/19/how-to-run-blender-headless-from-the-command-line-without-the-gui/>
  * It seems like the running of scripts is quite extensive thanks to python, so this again may be possible.
* ✓ We can install Add-ons automatically as well it seems, so there would be no need for manual configuration: <https://blender.stackexchange.com/questions/39253/can-an-add-on-be-automatically-installed-and-enabled>
* ✓ Step 3: Apply and preview the texture
  * Can we modify nodes with python scripts in Blender? Looks like it: <https://blender.stackexchange.com/questions/5413/how-to-connect-nodes-to-node-group-inputs-and-outputs-in-python>
* ✓ Step 4: Exporting to .smd and later .mdl
  * Since scripting is possible and the used Add-ons are open-source I am confident we can make this happen
  * ✓ Creating a rough collision model should be possible as well: <https://blender.stackexchange.com/questions/121778/convex-hull-as-mesh> (see script answer)
  * Use the Decimate Geometry command to reduce automatically to [around 30 convex parts](<https://developer.valvesoftware.com/wiki/Costly_collision_model>)
* ✓ Step 5: Compiling the .smd to an .mdl using a .qc
  * With some configuration beforehand (or by always making a static model) we can recycle the .qc example from the guide to make static models
  * (low priority) TODO: Investigate how more complex models work (like ragdolls, animated entities and other complex physics objects)
* ✓ Step 6: Converting the .tga textures to .vtf's with an accompanying .vmt
  * ✓ The .vmt would be simple to generate
  * ✓ To batch convert the .tga textures to .vtf's we can use either [Valve's vtex command-line tool (requires Steam to be running)](<https://developer.valvesoftware.com/wiki/Vtex>) or VTFCmd in [VTFLib](<http://nemesis.thewavelength.net/index.php?p=40>)
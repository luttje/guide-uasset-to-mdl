import bpy
from addon_utils import check, paths, enable

addon_dependencies = ['io_import_scene_unreal_psa_psk_280', 'io_scene_valvesource']

# Source: https://blender.stackexchange.com/questions/58202/how-can-i-import-an-addon-into-a-blender-script
def get_all_addons():
    import sys
    paths_list = paths()
    addon_list = []
    for path in paths_list:
        bpy.utils._sys_path_ensure(path)
        for mod_name, mod_path in bpy.path.module_names(path):
            is_enabled, is_loaded = check(mod_name)
            addon_list.append(mod_name)           
    return(addon_list)

addons = get_all_addons()

def perform_check():
    for addon in addon_dependencies:
        if addon in addons:
            is_enabled, is_loaded = check(addon)
            if not is_enabled:
                enable(addon)
        else:
            print("!Dependency %s missing!" % addon)

import { ModRegistrar } from "modding/types";
//import { HelloWorldComponent } from "mods/hello-world";
//import { MyGameModComponent } from "mods/game-mod";
//import { MyMenuModComponent } from "mods/menu-mod";
import { SkyveInstallButton } from "mods/skyveInstallButton";

const register: ModRegistrar = (moduleRegistry) => {
    // While launching game in UI development mode (include --uiDeveloperMode in the launch options)
    // - Access the dev tools by opening localhost:9444 in chrome browser.
    // - You should see a hello world output to the console.
    // - use the useModding() hook to access exposed UI, api and native coherent engine interfaces. 
    // Good luck and have fun!
    //moduleRegistry.append('Menu', HelloWorldComponent);
    //moduleRegistry.append("Menu", MyMenuModComponent);
    //moduleRegistry.append("Game", MyGameModComponent);
    moduleRegistry.extend(
        "game-ui/menu/components/main-menu-screen/main-menu-screen.tsx",
        "MainMenuNavigation",
        SkyveInstallButton
    );
}

export default register;
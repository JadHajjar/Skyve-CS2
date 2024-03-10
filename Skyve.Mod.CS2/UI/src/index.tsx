import { ModRegistrar } from "cs2/modding";
import { CustomMenuButton } from "menu-button/menu-button";

const register: ModRegistrar = (moduleRegistry) => {
  console.log("UI Modding playground");

  moduleRegistry.extend(
    "game-ui/menu/components/main-menu-screen/main-menu-screen.tsx",
    "MainMenuNavigation",
    CustomMenuButton
  );
};

export default register;

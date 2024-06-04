import { MenuButton } from "cs2/ui";
import skyveBack from "../images/skyve.png";
import { ModuleRegistryExtend } from "cs2/modding";
import { bindValue, trigger, useValue } from "cs2/api";

export const isInstalled$ = bindValue<boolean>("SkyveMod", "IsInstalled", true);
export const CustomMenuButton: ModuleRegistryExtend = (Component) => {
  return (props) => {
    const { children, ...otherProps } = props || {};
    const isInstalled = useValue(isInstalled$);

    if (isInstalled) return <Component {...otherProps}>{children}</Component>;

    return (
      <Component {...otherProps}>
        <MenuButton
          src={skyveBack}
          onSelect={() => trigger("SkyveMod", "InstallSkyve")}
        >
          Install Skyve
        </MenuButton>
        {children}
      </Component>
    );
  };
};

import { ModuleRegistryExtend } from "modding/types";
import { useModding } from "modding/modding-context";
import itemCss from "./skyveInstallButton.module.scss";
import { useCallback, MouseEvent } from "react";

export const SkyveInstallButton: ModuleRegistryExtend = (Component) => {
  return (props) => {
    const { children, ...otherProps } = props || {};
    const {
      api: {
        api: { useValue, bindValue, trigger },
      },
    } = useModding();

    const isInstalled = useValue(bindValue<boolean>("SkyveMod", "IsInstalled"));

    if (isInstalled) return <Component {...otherProps}>{children}</Component>;

    const executeMyCSMethod = useCallback(
      (ev: MouseEvent<HTMLDivElement>) => {
        trigger("SkyveMod", "InstallSkyve");
      },
      [trigger]
    );

    return (
      <Component {...otherProps}>
        <div className={itemCss.item} onClick={executeMyCSMethod}>
          <div className={itemCss.icon}>
            <div className={itemCss.icon1}></div>
            <div className={itemCss.icon2}></div>
          </div>
          <div className={itemCss.label}>Install Skyve</div>
        </div>
        {children}
      </Component>
    );
  };
};

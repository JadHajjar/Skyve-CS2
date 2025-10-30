import { MenuButton } from "cs2/ui";
import skyveBack from "../images/skyve.png";
import { ModuleRegistryExtend } from "cs2/modding";
import { bindValue, trigger, useValue } from "cs2/api";
import { useLocalization } from "cs2/l10n";

export const isInstalled$ = bindValue<boolean>("SkyveMod", "IsInstalled", true);
export const isUpToDate$ = bindValue<boolean>("SkyveMod", "IsUpToDate", true);
export const CustomMenuButton: ModuleRegistryExtend = (Component) => {
    return (props) => {
        const { children, ...otherProps } = props || {};
        const isInstalled = useValue(isInstalled$);
        const isUpToDate = useValue(isUpToDate$);
        const { translate } = useLocalization();

        if (isInstalled && isUpToDate) return <Component {...otherProps}>{children}</Component>;

        return (
            <Component {...otherProps}>
                <MenuButton
                    src={skyveBack}
                    onSelect={() => trigger("SkyveMod", "InstallSkyve")}
                >
                    {translate(`Options.OPTION[Skyve Mod.Skyve.Mod.CS2.SkyveMod.SkyveModSettings.${isUpToDate ? 'InstallApp' : 'UpdateApp'}]`)}
                </MenuButton>
                {children}
            </Component>
        );
    };
};

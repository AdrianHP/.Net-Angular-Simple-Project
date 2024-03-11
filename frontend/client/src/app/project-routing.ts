// Add routes to project-specific modules/components, placed into /src/app/project/ folder, like:
// { path: "my-feature", loadChildren: () => import("./my-feature/my-feature.module").then(m => m.MyFeatureModule) },

export const ProjectRoutes = [
    { path: "account", loadChildren: () => import("./account/account.module").then(m => m.AccountModule) },
];

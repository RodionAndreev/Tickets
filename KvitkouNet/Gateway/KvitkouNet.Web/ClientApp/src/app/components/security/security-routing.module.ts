import { UserComponent } from './user/user.component';
import { NewRoleComponent } from './new-role/new-role.component';
import { RolesComponent } from './roles/roles.component';
import { NewFunctionComponent } from './new-function/new-function.component';
import { FunctionsComponent } from './functions/functions.component';
import { NewFeatureComponent } from './new-feature/new-feature.component';
import { FeaturesComponent } from './features/features.component';
import { SecurityComponent } from './security.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NewRightComponent } from './new-right/new-right.component';
import { RightsComponent } from './rights/rights.component';
import { SecurityMenuComponent } from './security-menu/security-menu.component';

const routes: Routes = [
      { path: 'security',
        component: SecurityComponent,
        children: [
          { path: 'rights', component: RightsComponent },
          { path: 'rights/add', component: NewRightComponent },
          { path: 'features', component: FeaturesComponent },
          { path: 'features/add', component: NewFeatureComponent },
          { path: 'functions', component: FunctionsComponent },
          { path: 'functions/add', component: NewFunctionComponent },
          { path: 'roles', component: RolesComponent },
          { path: 'roles/add', component: NewRoleComponent },
          { path: 'user/:id', component: UserComponent }
        ]}
];

@NgModule({
  declarations: [

],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }

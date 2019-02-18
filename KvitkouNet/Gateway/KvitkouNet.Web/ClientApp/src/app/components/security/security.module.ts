import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SecurityMenuComponent } from './security-menu/security-menu.component';
import { NewRightComponent } from './new-right/new-right.component';
import { RightsComponent } from './rights/rights.component';

import { SecurityRoutingModule } from './security-routing.module';
import { FeaturesComponent } from './features/features.component';
import { NewFeatureComponent } from './new-feature/new-feature.component';
import { FunctionsComponent } from './functions/functions.component';
import { NewFunctionComponent } from './new-function/new-function.component';
import { RolesComponent } from './roles/roles.component';
import { NewRoleComponent } from './new-role/new-role.component';
import { FeatureService } from 'src/app/services/security/feature.service';
import { FunctionsService } from 'src/app/services/security/functions.service';
import { RightsService } from 'src/app/services/security/rights.service';
import { RoleService } from 'src/app/services/security/role.service';
import { UserRightsService } from 'src/app/services/security/userRights.service';
import { SecurityComponent } from './security.component';
import { UserComponent } from './user/user.component';

@NgModule({
  declarations: [
    NewRightComponent,
    RightsComponent,
    FeaturesComponent,
    NewFeatureComponent,
    FunctionsComponent,
    NewFunctionComponent,
    RolesComponent,
    NewRoleComponent,
    SecurityComponent,
    SecurityMenuComponent,
    UserComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SecurityRoutingModule
  ],
  providers: [FeatureService, FunctionsService, RightsService, RoleService, UserRightsService],
})
export class SecurityModule { }

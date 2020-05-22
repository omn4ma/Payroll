import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatNativeDateModule} from '@angular/material/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { TreeNestedOverviewExample as PersonsTreeComponent } from './components/persons-tree.component';
import {MAT_FORM_FIELD_DEFAULT_OPTIONS} from '@angular/material/form-field';
import { MyMaterialModule } from './material-module';
import { PayrollService } from './payroll-service';
import { ChooseDateDialogComponent } from './components/calculate-dialog.component';
import { CreatePersonDialogComponent } from './components/create-person-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    PersonsTreeComponent,
    ChooseDateDialogComponent,
    CreatePersonDialogComponent
  ],
  imports: [
    BrowserAnimationsModule,
    MatNativeDateModule,
    MyMaterialModule,
    ReactiveFormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule
  ],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
    PayrollService
  ],
    entryComponents: [PersonsTreeComponent],
  bootstrap: [AppComponent],
})
export class AppModule { }

import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Person } from '../payroll-service';

@Component({
    templateUrl: 'create-person-dialog.component.html',
  })
  export class CreatePersonDialogComponent {  
    newPerson: Person;

    constructor(
      public dialogRef: MatDialogRef<CreatePersonDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public head: Person) {}
  
    onNoClick(): void {
      this.dialogRef.close();
    }

  }
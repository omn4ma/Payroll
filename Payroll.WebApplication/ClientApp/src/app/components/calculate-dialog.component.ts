import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Moment } from 'moment';

@Component({
    templateUrl: 'calculate-dialog.component.html',
  })
  export class ChooseDateDialogComponent {  

    selectedDate : Moment;

    constructor(public dialogRef: MatDialogRef<ChooseDateDialogComponent>) {}
  
    onNoClick(): void {
      this.dialogRef.close();
    }
  }
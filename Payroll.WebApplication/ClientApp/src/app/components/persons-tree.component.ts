import {NestedTreeControl} from '@angular/cdk/tree';
import {Component} from '@angular/core';
import {MatTreeNestedDataSource} from '@angular/material/tree';
import { PayrollService, Person } from '../payroll-service';
import { MatDialog } from '@angular/material/dialog';
import { CreatePersonDialogComponent } from './create-person-dialog.component';
import { ChooseDateDialogComponent } from './calculate-dialog.component';

@Component({
  selector: 'persons-tree',
  templateUrl: 'persons-tree.component.html',
  styleUrls: ['persons-tree.component.css'],
})
export class TreeNestedOverviewExample {
  treeControl = new NestedTreeControl<Person>(node => node.staff);
  dataSource = new MatTreeNestedDataSource<Person>();
  service: PayrollService;
  public dialog: MatDialog;

  constructor(service : PayrollService, dialog: MatDialog) {
    this.dialog = dialog;
    this.service = service;
    service.getPersons().subscribe(result => this.dataSource.data = result, () => this.dataSource.data = []);
  }
  addStaff(head : Person) : void
  {
    const dialogRef = this.dialog.open(CreatePersonDialogComponent, {
      width: '700px',
      data: head
      });
  
      dialogRef.afterClosed().subscribe(result => {
          if (result) {
              this.service.savePerson(result).subscribe(s => {
                 head.staff.push(result);
                 result.id = s;
              }
            );
          }
      });
  }
  calculateSalary(node : Person) : void
  {
    const dialogRef = this.dialog.open(ChooseDateDialogComponent, { width: '700px' });
  
      dialogRef.afterClosed().subscribe(result => {
          if (result) {
              this.service.calculateSalary(node, result)
              .subscribe(s => alert(s));
          }
      });
  }
  getPositionName(node: Person) : string {
    return this.service.getPositionName(node.position);
  }
  canHire(node: Person): boolean {
    return node.position != 0;
  }
  hasChild = (_: number, node: Person) => !!node.staff && node.staff.length > 0;
}


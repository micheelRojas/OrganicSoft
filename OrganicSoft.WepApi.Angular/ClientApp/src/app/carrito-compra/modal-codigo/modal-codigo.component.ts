import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';


@Component({
  selector: 'app-modal-codigo',
  templateUrl: './modal-codigo.component.html',
  styleUrls: ['./modal-codigo.component.css']
})
export class ModalCodigoComponent implements OnInit {

  numero: number;

  constructor(
    public dialogRef: MatDialogRef<ModalCodigoComponent>
  ) { }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}

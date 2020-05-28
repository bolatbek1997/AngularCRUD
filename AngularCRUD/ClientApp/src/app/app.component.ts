import { Component, OnInit } from '@angular/core';
import { Model } from './model';
import { DataService } from './data.service.ts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  model: Model = new Model();   // изменяемый товар
  models: Model[];                // массив товаров
  tableMode: boolean = true;          // табличный режим

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.loadProducts();    // загрузка данных при старте компонента  
  }
  // получаем данные через сервис
  loadProducts() {
    this.dataService.getProducts()
      .subscribe((data: Model[]) => this.models = data);
  }
  // сохранение данных
  save() {
    if (this.model.id == null) {
      this.dataService.createProduct(this.model)
        .subscribe((data: Model) => this.models.push(data));
    } else {
      this.dataService.updateProduct(this.model)
        .subscribe(data => this.loadProducts());
    }
    this.cancel();
  }
  editProduct(p: Model) {
    this.model = p;
  }
  cancel() {
    this.model = new Model();
    this.tableMode = true;
  }
  delete(p: Model) {
    this.dataService.deleteProduct(p.id)
      .subscribe(data => this.loadProducts());
  }
  add() {
    this.cancel();
    this.tableMode = false;
  }
}

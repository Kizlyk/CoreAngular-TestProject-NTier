import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProductList } from './products/productList';
import { ProductView } from './products/productView';
import { ProductEdit } from './products/productEdit';

const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products', component: ProductList },
  { path: 'product/:id', component: ProductView },
  { path: 'product/edit/:id', component: ProductEdit }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }

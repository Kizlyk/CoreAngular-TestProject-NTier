import {Injectable, OnInit} from '@angular/core';
declare var $:any;

@Injectable()
export class Product {
    id:number = 0;
    code:string = null;
    name:string = null;
    photo:string = null;
    price:number = null;
    lastUpdated:Date = null;
}

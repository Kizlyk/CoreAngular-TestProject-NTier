import { Component, OnInit } from '@angular/core';
import { AppConfiguration } from "../appConfiguration";

@Component({
  selector: 'app-header',
  templateUrl: 'appHeader.html'
})
export class AppHeader implements OnInit {
  constructor(public config: AppConfiguration) {

  }

  ngOnInit() {

  }
}

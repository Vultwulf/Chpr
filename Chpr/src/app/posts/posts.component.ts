import { Component, OnInit, Input } from '@angular/core';
import { DataService } from '../data.service';
import { Observable } from 'rxjs';
import { trigger, style, transition, animate, keyframes, query, stagger } from '@angular/animations';
import { HttpErrorResponse } from '@angular/common/http';
import { Globals } from '../globals';
import { FormControl, NgModel } from '@angular/forms';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss'],
  animations: [
    trigger('listStagger', [
      transition('* <=> *', [
        query(
          ':enter',
          [
            style({ opacity: 0, transform: 'translateY(-15px)' }),
            stagger(
              '50ms',
              animate(
                '550ms ease-out',
                style({ opacity: 1, transform: 'translateY(0px)' })
              )
            )
          ], { optional: true }
        ),
        query(':leave', animate('50ms', style({ opacity: 0 })), {
          optional: true
        })
      ])
    ])
  ]
})

export class PostsComponent implements OnInit {

  posts$: Object;
  postLength = 0;

  constructor(private dataService: DataService, private globals: Globals) { }

  ngOnInit() {
    this.dataService.getPosts().subscribe(
      data => this.posts$ = data
    )

    // Reset the post length to 0
    this.postLength = 0;
  }

  // When the Post Model Changes
  OnChange(post: NgModel) {
    // Update the value with the current length
    this.postLength = post.value.length;
  }

  // On New Post Form Submit
  OnSubmit(post : NgModel) {
    this.dataService.savePost(post.value).subscribe((data: any) => {
      // Reinitialize ngOnInit
      this.ngOnInit();

      // Reset the form
      post.reset();
    });
  }
}

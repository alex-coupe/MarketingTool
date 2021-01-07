import React from 'react';
import './Grid.css';

export function GridContainer(props) {
   
    return (
        <div style={props.style} id={props.id} className={`grid-container ${props.className ? props.className : ''}`}>
            {props.children}
        </div>
    )
}

export function GridItem(props) {
    return (
        <div style={props.style} id={props.id} className={props.className} >
            {props.children}
        </div>
    )
}
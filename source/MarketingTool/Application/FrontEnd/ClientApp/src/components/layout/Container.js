import React from 'react';

export default function Container(props) {
    return (
        <div style={props.style} id={props.id} className={`container ${props.className}`} >
            {props.children}
        </div>
    )
}


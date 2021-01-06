import React from 'react';

export default function Button(props) {
    return (
        <div>
            <button type={props.type} className={props.className} name={props.name} onClick={props.onClick} style={props.style}>{props.display}{props.children}</button>
        </div>
    )
}
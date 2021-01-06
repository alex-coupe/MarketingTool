import React from 'react';

export function Card(props) {
    return (
        <div>
            <div className={`card ` + props.className} style={props.style}>
                <div>{props.children ? props.children : null}</div>
            </div>
        </div>
    )
}

export function CardTitle(props) {
    return (
        <h2 style={props.style} className={`card-header ` + props.className}>{props.title} {props.children}</h2>
    )
}

export function CardBody(props) {
    return (
        <div style={props.style} className={`card-body ` + props.className}>
            {props.children ? props.children : null}
        </div>
    )
}
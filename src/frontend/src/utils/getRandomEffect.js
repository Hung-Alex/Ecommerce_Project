// utils/getRandomEffect.js
const effectClasses = ['fade-up', 'slide-left', 'zoom-in', 'rotate', 'glow', 'flip', 'blink', 'pulse', 'float'];

export const getRandomEffect = () => {
  return effectClasses[Math.floor(Math.random() * effectClasses.length)];
};
